using System;
using System.Linq;
using System.Reflection;
using ComposableWebApplication.SDK.Core;
using ComposableWebApplication.SDK.Web.Utility;
using ComposableWebApplication.SDK.Web.ViewLoader;
using Microsoft.AspNetCore.Builder;
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
				foreach (var exportedType in assembly.ExportedTypes)
				{
					if (typeof(IFeature).IsAssignableFrom(exportedType))
					{
						var feature = Activator.CreateInstance(exportedType) as IFeature;
						if (feature == null)
							throw new ArgumentNullException(nameof(feature), $"{nameof(feature)}");
						source.AddSingleton(feature);
						feature.Register(source);
					}

					if (typeof(INamedViewLoader).IsAssignableFrom(exportedType))
					{
						var feature = Activator.CreateInstance(exportedType) as INamedViewLoader;
						if (feature == null)
							throw new ArgumentNullException(nameof(feature), $"{nameof(feature)}");
						source.AddSingleton(feature);
					}
				}
			}

			return source;
		}
	}
}