using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ComposableWebApplication.Plugin3.MyFeature.Pages
{
	public class Page1Model : PageModel
	{
		public void OnGet()
		{
			var a = JsonConvert.SerializeObject("string");
		}
	}
}