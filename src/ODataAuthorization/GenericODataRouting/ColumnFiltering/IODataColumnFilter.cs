using System.Linq;

namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	/// <summary>
	/// This interface is used to manually restrict an entity and which properties are being rendered into OData requests.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IODataColumnFilter<TEntity>
	{
		IQueryable Apply(IQueryable<TEntity> source);
		object Apply(TEntity source);
	}
}