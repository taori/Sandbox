using System.Collections.Generic;

namespace ODataAuthorization.GenericODataRouting.ColumnFiltering
{
	/// <summary>
	/// A column whitelist can be implemented multiple times and lists the property names which are used to render the properties of an OData entity
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IODataColumnWhitelist<TEntity>
	{
		IEnumerable<string> GetPropertyNames();
	}
}