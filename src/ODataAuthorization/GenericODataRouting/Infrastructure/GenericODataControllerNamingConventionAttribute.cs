using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ODataAuthorization.GenericODataRouting.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class GenericODataControllerNamingConventionAttribute : Attribute, IControllerModelConvention
	{
		public void Apply(ControllerModel controller)
		{
			if (controller.ControllerType.GetGenericTypeDefinition() !=
			    typeof(GenericODataController<>))
			{
				// Not a GenericController, ignore.
				return;
			}

			var entityType = controller.ControllerType.GenericTypeArguments[0];
			controller.ControllerName = entityType.Name;
		}
	}
}