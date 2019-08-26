using Microsoft.AspNetCore.Mvc;

namespace ComposableWebApplication.Plugin1.Controllers
{
	[Route("[controller]")]
	public class Feature1Controller : Controller
	{
		public IActionResult Index()
		{
			return Ok("Hello from feature 1");
		}

		[Route("[action]")]
		public IActionResult Sub()
		{
			return View();
		}
	}
}