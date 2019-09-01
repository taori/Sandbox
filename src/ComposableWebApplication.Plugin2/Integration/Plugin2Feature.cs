using System;
using ComposableWebApplication.SDK.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.Plugin2
{
	public class Plugin2Feature : IFeature
	{
		/// <inheritdoc />
		public string Name => "Plugin 2";

		/// <inheritdoc />
		public void Register(IServiceCollection serviceProvider)
		{
		}
	}
}
