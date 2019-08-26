using ComposableWebApplication.SDK.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.Plugin3
{
	public class Feature : IFeature
	{
		/// <inheritdoc />
		public string Name => "Plugin 3";

		/// <inheritdoc />
		public void Register(IServiceCollection serviceProvider)
		{
		}
	}
}