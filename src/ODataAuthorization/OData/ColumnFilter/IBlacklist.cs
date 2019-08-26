using System.Collections.Generic;

namespace ODataAuthorization.OData.ColumnFilter
{
	/// <summary>
	/// A column blacklist can be implemented multiple times and restricts the properties which can be rendered into a OData request.
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IBlacklist<TEntity> : IBlacklist
	{
		IEnumerable<string> GetPropertyNames();
	}

	/// <summary>
	/// A column blacklist can be implemented multiple times and restricts the properties which can be rendered into a OData request.
	/// </summary>
	public interface IBlacklist
	{
		IEnumerable<string> GetPropertyNames();
	}
}