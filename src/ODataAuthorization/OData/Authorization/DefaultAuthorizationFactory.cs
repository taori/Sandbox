using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ODataAuthorization.OData.Authorization
{
	internal class DefaultAuthorizationFactory : IAuthorizationFactory
	{
		public ImmutableArray<IEntityAuthorization> Authorizations { get; }

		public DefaultAuthorizationFactory(IEnumerable<IEntityAuthorization> authorizations)
		{
			Authorizations = authorizations.ToImmutableArray();
		}

		/// <inheritdoc />
		public IEnumerable<IEntityAuthorization<TEntity>> Create<TEntity>()
		{
			return Authorizations.Where(d => d is IEntityAuthorization<TEntity>).Cast<IEntityAuthorization<TEntity>>();
		}
	}
}