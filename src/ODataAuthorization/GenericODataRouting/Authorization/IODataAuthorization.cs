using System.Linq;

namespace ODataAuthorization.GenericODataRouting.Authorization
{
	/// <summary>
	/// This is a processing interface to add permission checks on an entity level to limit the results you can obtain through OData requests.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IODataAuthorization<TEntity>
	{
		IQueryable<TEntity> BatchApply(IQueryable<TEntity> source, IODataAuthorizationContext context);
		bool Apply(TEntity source, IODataAuthorizationContext context);
	}
}