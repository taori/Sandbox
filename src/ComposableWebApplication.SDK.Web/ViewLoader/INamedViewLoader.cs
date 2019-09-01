using Microsoft.AspNetCore.Mvc;

namespace ComposableWebApplication.SDK.Web.ViewLoader
{
	public interface INamedViewLoader
	{
		ViewResult GetView(string viewName, object parameter);
	}
}