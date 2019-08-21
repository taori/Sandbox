using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorization.Models;

namespace ODataAuthorization.GenericODataRouting
{
	public interface IODataAuthorization<TEntity>
	{
		IQueryable<TEntity> BatchApply(IQueryable<TEntity> source, IODataAuthorizationContext context);
		bool Apply(TEntity source, IODataAuthorizationContext context);
	}

	public enum ODataAuthorizationType
	{
		GetAll,
		GetId,
		Delete,
		Update
	}

	public interface IODataAuthorizationContext
	{
		ODataAuthorizationType AuthorizationType { get; }
	}

	public class ODataAuthorizationContext : IODataAuthorizationContext
	{
		/// <inheritdoc />
		public ODataAuthorizationType AuthorizationType { get; private set; }

		public static ODataAuthorizationContext FromType(ODataAuthorizationType type)
		{
			var context = new ODataAuthorizationContext();
			context.AuthorizationType = type;
			return context;
		}

		private ODataAuthorizationContext()
		{
		}
	}

	[EnableQuery]
	[GenericODataControllerNamingConvention]
	[Produces("application/json")]
	[Route("api/odata/[controller]")]
	public class GenericODataController<TEntity> : ODataController
		where TEntity : class
	{
		public AdventureWorksContext Context { get; }

		public ImmutableArray<IODataAuthorization<TEntity>> Authorizations { get; }

		public GenericODataController(AdventureWorksContext context, IEnumerable<IODataAuthorization<TEntity>> authorizations)
		{
			Context = context;
			Authorizations = authorizations.ToImmutableArray();
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

		public IQueryable<TEntity> All() => ApplyAuthorizations(Context.Set<TEntity>().AsQueryable(), ODataAuthorizationContext.FromType(ODataAuthorizationType.GetAll));

		[Route("{id}")]
		public async Task<ActionResult<TEntity>> Id(int id)
		{
			var entity = await Context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
			if (entity == null)
				return NotFound(default);

			if (ApplyAuthorization(entity, ODataAuthorizationContext.FromType(ODataAuthorizationType.GetId)) == default)
				return Unauthorized();

			return entity;
		}
	}
}