using Microsoft.Extensions.DependencyInjection;

namespace ODataAuthorization.GenericODataRouting
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddGenericODataRouting (this IMvcBuilder source)
		{
			source.ConfigureApplicationPartManager(configure =>
			{
				configure.FeatureProviders.Add(new GenericODataControllerFeature());
			});

			return source;
		}
	}
}