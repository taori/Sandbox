using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComposableWebApplication.SDK.Web.Utility
{
	public static class PluginDirectory
	{
		public static IEnumerable<string> GetAssemblyPaths(string path)
		{
			return Directory
				.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
				.Where(d => !d.Contains($"{Path.DirectorySeparatorChar}refs{Path.DirectorySeparatorChar}"));
		}
	}
}