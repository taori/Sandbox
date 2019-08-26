using Microsoft.Extensions.DependencyInjection;
using ODataAuthorization.OData.Authorization;
using ODataAuthorization.OData.ColumnFilter;

namespace ODataAuthorization.OData.Infrastructure
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddGenericODataApi (this IMvcBuilder source)
		{
			source.ConfigureApplicationPartManager(configure =>
			{
				configure.FeatureProviders.Add(new GenericODataControllerFeature());
			});

			source.Services.AddScoped<IColumnSelector, DefaultColumnSelector>();
			source.Services.AddScoped<IAuthorizationFactory, DefaultAuthorizationFactory>();
			source.Services.AddScoped<IAuthorizer, DefaultAuthorizer>();

			return source;
		}
	}
}