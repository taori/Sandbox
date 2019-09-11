using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace ComposableWebApplication.Plugin2.Views.Shared.Components.Plugin2
{
	[ViewComponent(Name = nameof(ComposableWebApplication.Plugin2.Views.Shared.Components.Plugin2))]
	public class Plugin2ViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}