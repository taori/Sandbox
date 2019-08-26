using System.Linq;

namespace ODataAuthorization.OData.Authorization
{
	/// <summary>
	/// This is a processing interface to add permission checks on an entity level to limit the results you can obtain through OData requests.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IEntityAuthorization<TEntity> : IEntityAuthorization
	{
		IQueryable<TEntity> BatchApply(IQueryable<TEntity> source, IAuthorizationContext context);
		bool IsAuthorized(TEntity source, IAuthorizationContext context);
	}

	/// <summary>
	/// Marker interface - use <see cref="IEntityAuthorization{TEntity}"/> for actual implementations
	/// </summary>
	public interface IEntityAuthorization { }
}