using System.Collections.Generic;

namespace ODataAuthorization.OData.Authorization
{
	public interface IAuthorizationFactory
	{
		IEnumerable<IEntityAuthorization<TEntity>> Create<TEntity>();
	}
}