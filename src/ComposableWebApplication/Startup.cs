using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ComposableWebApplication.SDK.Web.Extensions;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ComposableWebApplication
{
	public class DateTimeOffsetConstraint : IRouteConstraint
	{
		/// <inheritdoc />
		public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (values.TryGetValue(routeKey, out var routeValue))
			{
				var routeValueString = routeValue?.ToString();
				if (string.IsNullOrEmpty(routeValueString))
					return false;

				if (DateTimeOffset.TryParse(routeValueString, out var parsed))
				{
					return true;
				}
			}

			return false;
		}
	}

	public class DateTimeOffsetModelBinder : IModelBinder, IModelBinderProvider
	{
		/// <inheritdoc />
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext.ActionContext.RouteData.Values.TryGetValue(bindingContext.FieldName, out var value))
			{
				var valueString = value.ToString();
				if (DateTimeOffset.TryParse(valueString, out var parsed))
					bindingContext.Result = ModelBindingResult.Success(parsed);
			}

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			if (context.Metadata?.ModelType == typeof(DateTimeOffset))
			{
				return new DateTimeOffsetModelBinder();
			}

			return null;
		}
	}

	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			HostingEnvironment = hostingEnvironment;
		}

		public IConfiguration Configuration { get; }

		public IHostingEnvironment HostingEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.Configure<RouteOptions>(options =>
			{
				options.ConstraintMap.Add("dateTimeOffset", typeof(DateTimeOffsetConstraint));
			});

			var pluginRoot = Path.Combine(HostingEnvironment.ContentRootPath, "Plugins");

			services
				.AddMvc()
				.AddPlugins(pluginRoot)
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
