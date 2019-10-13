using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using System.Threading.Tasks;
using LinkGenerator.ReferencedProject.MyFeature.Pages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LinkGenerator.Application
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var applicationPartAssemblies = new[]
			{
				typeof(ReferencedPageModel).Assembly,
				AssemblyLoadContext.Default.LoadFromAssemblyPath(@"D:\GitHub\Sandbox\src\LinkGenerator.RuntimeLoadedProject\bin\Debug\netstandard2.0\LinkGenerator.RuntimeLoadedProject.dll")
			};

			services.AddRazorPages()
				.ConfigureApplicationPartManager(configure =>
				{
					foreach (var assembly in applicationPartAssemblies)
					{
						AddParts(configure.ApplicationParts, assembly);
					}
				});
		}

		private void AddParts(IList<ApplicationPart> partList, Assembly assembly)
		{
			var factory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
			var parts = factory.GetApplicationParts(assembly);
			foreach (var part in parts)
			{
				partList.Add(part);
			}

			var related = RelatedAssemblyAttribute.GetRelatedAssemblies(assembly, true);
			foreach (var refAssembly in related)
			{
				AddParts(partList, refAssembly);
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
