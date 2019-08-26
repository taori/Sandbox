using System.Linq;

namespace ODataAuthorization.OData.Authorization
{
	/// <summary>
	/// This interface is used to apply authorization logic
	/// </summary>
	public interface IAuthorizer
	{
		IQueryable<TEntity> Authorize<TEntity>(IQueryable<TEntity> items, IAuthorizationContext context);
		TEntity Authorize<TEntity>(TEntity item, IAuthorizationContext context);
	}
}