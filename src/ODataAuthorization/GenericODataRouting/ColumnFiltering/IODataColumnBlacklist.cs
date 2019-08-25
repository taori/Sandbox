using System.Collections.Generic;

namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	/// <summary>
	/// A column blacklist can be implemented multiple times and restricts the properties which can be rendered into a OData request.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IODataColumnBlacklist<TEntity>
	{
		IEnumerable<string> GetPropertyNames();
	}
}