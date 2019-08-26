using System.Collections.Generic;
using System.Linq;
using ComposableWebApplication.SDK.Core;
using Microsoft.AspNetCore.Mvc;

namespace ComposableWebApplication.Controllers
{
	public class FeatureController : Controller
	{
		private readonly IEnumerable<IFeature> _features;

		public FeatureController(IEnumerable<IFeature> features)
		{
			_features = features;
		}

		public IActionResult Index()
		{
			return Content($"Loaded features are: {string.Join(',', _features.Select(d => d.Name))}");
		}
	}
}