using System.Collections.Immutable;
using System.Linq;

namespace ODataAuthorization.OData.Authorization
{
	internal class DefaultAuthorizer : IAuthorizer
	{
		public IAuthorizationFactory AuthorizationFactory { get; }

		public DefaultAuthorizer(IAuthorizationFactory authorizationFactory)
		{
			AuthorizationFactory = authorizationFactory;
		}

		/// <inheritdoc />
		public IQueryable<TEntity> Authorize<TEntity>(IQueryable<TEntity> items, IAuthorizationContext context)
		{
			var latest = items;
			var authorizations = AuthorizationFactory.Create<TEntity>();
			foreach (var authorization in authorizations)
			{
				latest = authorization.BatchApply(latest, context);
			}
			return latest;
		}

		/// <inheritdoc />
		public TEntity Authorize<TEntity>(TEntity item, IAuthorizationContext context)
		{
			var authorizations = AuthorizationFactory.Create<TEntity>().ToImmutableArray();
			return (authorizations.Length == 0 || authorizations.All(d => d.IsAuthorized(item, context))) ? item : default;
		}
	}
}