using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorization.GenericODataRouting.Authorization;
using ODataAuthorization.GenericODataRouting.ColumnFiltering;
using ODataAuthorization.Models;

namespace ODataAuthorization.GenericODataRouting.Infrastructure
{
	[EnableQuery]
	[GenericODataControllerNamingConvention]
	[Produces("application/json")]
	[Route("api/odata/[controller]")]
	public class GenericODataController<TEntity> : ODataController
		where TEntity : class
	{
		public AdventureWorksContext Context { get; }

		public IODataColumnFilterFactory ColumnFilterFactory { get; }

		public ImmutableArray<IODataAuthorization<TEntity>> Authorizations { get; }

		public GenericODataController(AdventureWorksContext context, IEnumerable<IODataAuthorization<TEntity>> authorizations, IODataColumnFilterFactory columnFilterFactory)
		{
			Context = context;
			ColumnFilterFactory = columnFilterFactory;
			Authorizations = authorizations.ToImmutableArray();
		}

		private IQueryable ApplyColumnFilter(IQueryable<TEntity> source)
		{
			if (ColumnFilterFactory == null)
				return source;

			var filter = ColumnFilterFactory.Create<TEntity>();
			return filter.Apply(source);
		}

		private object ApplyColumnFilter(TEntity source)
		{
			if (ColumnFilterFactory == null)
				return source;

			var filter = ColumnFilterFactory.Create<TEntity>();
			return filter.Apply(source);
		}

		private TEntity ApplyAuthorization(TEntity source, IODataAuthorizationContext context)
		{
			return (Authorizations.Length == 0 || Authorizations.All(d => d.Apply(source, context))) ? source : default;
		}

		private IQueryable<TEntity> ApplyAuthorizations(IQueryable<TEntity> source, IODataAuthorizationContext context)
		{
			var latest = source;
			foreach (var authorization in Authorizations)
			{
				latest = authorization.BatchApply(latest, context);
			}
			return latest;
		}

		[Route("[action]")]
		public ActionResult<IQueryable<TEntity>> List()
		{
			var items = Context.Set<TEntity>().AsQueryable();
			if (!items.Any())
				return NotFound(default);

			var authorized = ApplyAuthorizations(items, ODataAuthorizationContext.FromType(AuthorizationType.List));
			if (!authorized.Any())
				return NotFound(default);

			return Ok(ApplyColumnFilter(authorized));
		}

		[Route("{id}")]
		public async Task<ActionResult<object>> Id(int id)
		{
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if (ApplyAuthorization(entity, ODataAuthorizationContext.FromType(AuthorizationType.GetById)) == default)
				return Unauthorized();

			return ApplyColumnFilter(entity);
		}
	}
}