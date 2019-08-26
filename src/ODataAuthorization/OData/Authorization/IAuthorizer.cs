using System.Linq;

namespace ODataAuthorization.OData.Authorization
{
	public interface IAuthorizer
	{
		IQueryable<TEntity> Authorize<TEntity>(IQueryable<TEntity> items, IAuthorizationContext context);
		TEntity Authorize<TEntity>(TEntity item, IAuthorizationContext context);
	}
}