using System.Linq;

namespace ODataAuthorization.OData.ColumnFilter
{
	public interface IColumnSelector
	{
		IQueryable FromMultiple<TEntity>(IQueryable<TEntity> source);
		object FromSingle<TEntity>(TEntity source);
	}
}