using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.SDK.Web.Extensions
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddPluginControllers(this IMvcBuilder source, string path)
		{
			var assemblyPaths = PluginDirectory.GetAssemblyPaths(path).ToDictionary(d => d, Assembly.LoadFile);


			// attempt 1
			source.ConfigureApplicationPartManager(manager =>
			{
				foreach (var assemblyPath in assemblyPaths)
				{
					if (assemblyPath.Key.EndsWith("Views.dll"))
					{
						manager.ApplicationParts.Add(new CompiledRazorAssemblyPart(assemblyPath.Value));
					}
					else
					{
						manager.ApplicationParts.Add(new AssemblyPart(assemblyPath.Value));
					}
				}
			});

			// attempt 2
			//			source.AddRazorOptions(options =>
			//			{
			//				foreach (var assemblyPath in assemblyPaths)
			//				{
			//					if (!assemblyPath.Key.EndsWith("Views.dll"))
			//						options.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(assemblyPath.Value.Location));
			//				}
			//			});
			//			foreach (var assemblyPath in assemblyPaths)
			//			{
			//				if (assemblyPath.Key.EndsWith("Views.dll"))
			//				{
			//					var partFactory = ApplicationPartFactory.GetApplicationPartFactory(assemblyPath.Value);
			//					foreach (var part in partFactory.GetApplicationParts(assemblyPath.Value))
			//					{
			//						source.PartManager.ApplicationParts.Add(part);
			//					}
			//
			//					var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(assemblyPath.Value, throwOnError: true);
			//					foreach (var assembly in relatedAssemblies)
			//					{
			//						partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
			//						foreach (var part in partFactory.GetApplicationParts(assembly))
			//						{
			//							source.PartManager.ApplicationParts.Add(part);
			//						}
			//					}
			//				}
			//				else
			//				{
			//					source.PartManager.ApplicationParts.Add(new AssemblyPart(assemblyPath.Value));
			//				}
			//			}

			// attempt 3
			//			foreach (var assemblyPath in assemblyPaths)
			//			{
			//				source.AddApplicationPart(assemblyPath.Value);
			//			}

			return source;
		}
	}
}