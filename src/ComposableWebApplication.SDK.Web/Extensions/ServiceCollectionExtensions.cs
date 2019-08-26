using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ComposableWebApplication.SDK.Core;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.SDK.Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPluginFolder(this IServiceCollection source, string path)
		{
			var assemblyPaths = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
			foreach (var assemblyPath in assemblyPaths)
			{
				var assembly = Assembly.LoadFile(assemblyPath);
				var featureTypes = assembly.ExportedTypes.Where(d => typeof(IFeature).IsAssignableFrom(d));
				foreach (var featureType in featureTypes)
				{
					var feature = Activator.CreateInstance(featureType) as IFeature;
					source.AddSingleton(feature);
					feature.Register(source);
				}
			}

			return source;
		}
	}

	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddPluginControllers(this IMvcBuilder source, string path)
		{
			var assemblyPaths = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
			foreach (var assemblyPath in assemblyPaths)
			{
				source.PartManager.ApplicationParts.Add(new AssemblyPart(Assembly.LoadFile(assemblyPath)));
			}

			return source;
		}
	}
}