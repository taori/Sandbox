using System;
using System.Linq;
using System.Reflection;
using ComposableWebApplication.SDK.Core;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.SDK.Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPluginFolder(this IServiceCollection source, string path)
		{
			var assemblyPaths = PluginDirectory.GetAssemblyPaths(path);
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
}