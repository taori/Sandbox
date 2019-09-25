using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using ComposableWebApplication.SDK.Web.Extensions;
using ComposableWebApplication.SDK.Web.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace ComposableWebApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			HostingEnvironment = hostingEnvironment;
		}

		public IConfiguration Configuration { get; }

		public IWebHostEnvironment HostingEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddRazorPages();

			var pluginRoot = Path.Combine(HostingEnvironment.ContentRootPath, "Plugins");

			var assemblyPaths = PluginDirectory.GetAssemblyPaths(pluginRoot).ToDictionary(d => d, AssemblyLoadContext.Default.LoadFromAssemblyPath);

			services
				.AddControllersWithViews()
				.AddPlugins(assemblyPaths)
				.AddRazorRuntimeCompilation(config =>
				{
					foreach (var assemblyPath in assemblyPaths.Keys)
					{
						config.AdditionalReferencePaths.Add(assemblyPath);
					}

					config.FileProviders.Add(new PhysicalFileProvider(@"D:\GitHub\Sandbox\src\ComposableWebApplication.Plugin1"));
					config.FileProviders.Add(new PhysicalFileProvider(@"D:\GitHub\Sandbox\src\ComposableWebApplication.Plugin2"));
					config.FileProviders.Add(new PhysicalFileProvider(@"D:\GitHub\Sandbox\src\ComposableWebApplication.Plugin3"));
				});
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

//			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseEndpoints(d =>
			{
				d.MapRazorPages();

				d.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
