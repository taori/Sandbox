using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ComposableWebApplication.SDK.Core;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ComposableWebApplication.SDK.Web.Extensions
{
	public class CustomAssemblyPart : ApplicationPart, ICompilationReferencesProvider
	{
		private readonly AssemblyPart _assemblyPart;

		public CustomAssemblyPart(AssemblyPart assemblyPart)
		{
			_assemblyPart = assemblyPart;
		}

		/// <inheritdoc />
		public IEnumerable<string> GetReferencePaths()
		{
			yield return _assemblyPart.Assembly.Location;
		}

		/// <inheritdoc />
		public override string Name => _assemblyPart.Assembly.GetName().Name;
	}

	public class CustomRazorAssemblyPart : ApplicationPart, ICompilationReferencesProvider, IRazorCompiledItemProvider
	{
		private readonly CompiledRazorAssemblyPart _assemblyPart;

		public CustomRazorAssemblyPart(CompiledRazorAssemblyPart assemblyPart)
		{
			_assemblyPart = assemblyPart;
		}

		/// <inheritdoc />
		public IEnumerable<string> GetReferencePaths()
		{
			yield return _assemblyPart.Assembly.Location;
		}

		/// <inheritdoc />
		public IEnumerable<RazorCompiledItem> CompiledItems => (_assemblyPart as IRazorCompiledItemProvider).CompiledItems;

		/// <inheritdoc />
		public override string Name => _assemblyPart.Assembly.GetName().Name;
	}

	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddPlugins(this IMvcBuilder source, Dictionary<string, Assembly> assemblyPaths)
		{
			source.ConfigureApplicationPartManager(manager =>
			{
				foreach (var assemblyPath in assemblyPaths)
				{
					if (assemblyPath.Key.EndsWith("Views.dll"))
					{
						manager.ApplicationParts.Add(new CustomRazorAssemblyPart(new CompiledRazorAssemblyPart(assemblyPath.Value)));
					}
					else
					{
						manager.ApplicationParts.Add(new CustomAssemblyPart(new AssemblyPart(assemblyPath.Value)));
					}

					var featureTypes = assemblyPath.Value.ExportedTypes.Where(d => !d.IsInterface && typeof(IFeature).IsAssignableFrom(d));
					foreach (var featureType in featureTypes)
					{
						var feature = Activator.CreateInstance(featureType) as IFeature;
						if (feature == null)
							throw new ArgumentNullException(nameof(feature), $"Failed to instantiate {nameof(IFeature)} from type {featureType.FullName}.");

						source.Services.AddSingleton(feature);
						feature.Register(source.Services);
					}
				}
			});

			return source;
		}
	}
}