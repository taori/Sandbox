using System.Collections.Generic;

namespace ODataAuthorization.OData.ColumnFilter
{
	/// <summary>
	/// A column whitelist can be implemented multiple times and lists the property names which are used to render the properties of an OData entity
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IWhitelist<TEntity> : IWhitelist
	{
		IEnumerable<string> GetPropertyNames();
	}

	/// <summary>
	/// Marker interface - use <see cref="IWhitelist{TEntity}"/> for actual implementations
	/// </summary>
	public interface IWhitelist { }
}