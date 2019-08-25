using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorization.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ODataAuthorization.GenericODataRouting
{
	public interface IODataAuthorization<TEntity>
	{
		IQueryable<TEntity> BatchApply(IQueryable<TEntity> source, IODataAuthorizationContext context);
		bool Apply(TEntity source, IODataAuthorizationContext context);
	}

	public interface IODataColumnWhitelist<TEntity>
	{
		IEnumerable<string> GetPropertyNames();
	}

	public interface IODataColumnBlacklist<TEntity>
	{
		IEnumerable<string> GetPropertyNames();
	}

	public interface IODataColumnFilter<TEntity>
	{
		IQueryable Apply(IQueryable<TEntity> source);
		object Apply(TEntity source);
	}

	internal class DefaultODataColumnFilter<TEntity> : IODataColumnFilter<TEntity>
	{
		public IServiceProvider ServiceProvider { get; }

		private readonly string SelectExpression;

		public DefaultODataColumnFilter(IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
			SelectExpression = BuildExpression();
		}

		private string BuildExpression()
		{
			var whitelists = ServiceProvider.GetServices<IODataColumnWhitelist<TEntity>>();
			var blacklists = ServiceProvider.GetServices<IODataColumnBlacklist<TEntity>>();
			var columnMappings = GetColumnMappings();
			return "new(AddressId as addressId, City)";
		}

		private IEnumerable<(string propertyName, string serializationName)> GetColumnMappings()
		{
			return typeof(TEntity).GetProperties().Select(d => (d.Name, ToCamelCase(d.Name)));
		}

		private string ToCamelCase(string source)
		{
			return source.Substring(0, 1).ToLowerInvariant() + source.Substring(1);
		}

		/// <inheritdoc />
		public IQueryable Apply(IQueryable<TEntity> source)
		{
			return source.Select(SelectExpression);
		}

		/// <inheritdoc />
		public object Apply(TEntity source)
		{
			return new[] { source }.AsQueryable().Select(SelectExpression);
		}
	}

	public enum ODataAuthorizationType
	{
		List,
		GetById,
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

		public IODataColumnFilter<TEntity> ColumnFilter { get; }

		public ImmutableArray<IODataAuthorization<TEntity>> Authorizations { get; }

		public GenericODataController(AdventureWorksContext context, IEnumerable<IODataAuthorization<TEntity>> authorizations, IODataColumnFilter<TEntity> columnFilter)
		{
			Context = context;
			ColumnFilter = columnFilter;
			Authorizations = authorizations.ToImmutableArray();
		}

		private IQueryable ApplyColumnFilter(IQueryable<TEntity> source)
		{
			if (ColumnFilter == null)
				return source;

			return ColumnFilter.Apply(source);
		}

		private object ApplyColumnFilter(TEntity source)
		{
			if (ColumnFilter == null)
				return source;

			return ColumnFilter.Apply(source);
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

			var authorized = ApplyAuthorizations(items, ODataAuthorizationContext.FromType(ODataAuthorizationType.List));
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

			if (ApplyAuthorization(entity, ODataAuthorizationContext.FromType(ODataAuthorizationType.GetById)) == default)
				return Unauthorized();

			return ApplyColumnFilter(entity);
		}
	}
}