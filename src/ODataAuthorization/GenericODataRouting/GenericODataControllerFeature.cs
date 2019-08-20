using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataAuthorization.Models;

namespace ODataAuthorization.GenericODataRouting
{
	public class GenericODataControllerFeature : IApplicationFeatureProvider<ControllerFeature>
	{
		/// <inheritdoc />
		public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
		{
			foreach (var property in typeof(AdventureWorksContext).GetProperties())
			{
				if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
					feature.Controllers.Add(typeof(GenericODataController<>).MakeGenericType(property.PropertyType.GenericTypeArguments[0]).GetTypeInfo());
			}
		}
	}
}