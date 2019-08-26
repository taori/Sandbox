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
	/// Marker interface - use <see cref="IBlacklist{TEntity}"/> for actual implementations
	/// </summary>
	public interface IBlacklist
	{
	}
}