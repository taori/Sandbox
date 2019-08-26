using System.Reflection;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.SDK.Web.Extensions
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddPluginControllers(this IMvcBuilder source, string path)
		{
			var assemblyPaths = PluginDirectory.GetAssemblyPaths(path);

			source.ConfigureApplicationPartManager(manager =>
			{
				foreach (var assemblyPath in assemblyPaths)
				{
					if (assemblyPath.EndsWith("Views.dll"))
					{
						manager.ApplicationParts.Add(new CompiledRazorAssemblyPart(Assembly.LoadFile(assemblyPath)));
					}
					else
					{
						manager.ApplicationParts.Add(new AssemblyPart(Assembly.LoadFile(assemblyPath)));
					}
				}
			});

			return source;
		}
	}
}