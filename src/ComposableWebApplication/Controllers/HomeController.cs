using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposableWebApplication.Models;

namespace ComposableWebApplication.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[Route("[controller]/[action]/{now:dateTimeOffset}")]
		public IActionResult DateTimeOffsetConstraint(DateTimeOffset now)
		{
			return View(now);
		}

		[Route("[controller]/[action]/{now:dateTime}")]
		public IActionResult DateTimeConstraint(DateTime now)
		{
			return View(now);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
