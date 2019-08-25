using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ODataAuthorization.GenericODataRouting;
using ODataAuthorization.GenericODataRouting.Authorization;
using ODataAuthorization.GenericODataRouting.ColumnFiltering;
using ODataAuthorization.GenericODataRouting.Infrastructure;
using ODataAuthorization.Models;

namespace ODataAuthorization
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
			services.AddOData();

			services.AddScoped<IODataColumnFilterFactory, DefaultColumnFilterFactory>();
			services.AddScoped<IODataAuthorization<Address>, AddressAuthorization>();
			services.AddScoped<IODataColumnWhitelist<Address>, AddressWhitelist1>();
			services.AddScoped<IODataColumnWhitelist<Address>, AddressWhitelist2>();
			services.AddScoped<IODataColumnBlacklist<Address>, AddressBlacklist1>();

			services.AddDbContext<AdventureWorksContext>()
				.AddMvc(options => { options.EnableEndpointRouting = false; })
				.AddGenericODataRouting()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			var odataModelBuilder = new ODataConventionModelBuilder(app.ApplicationServices);

			app.UseHttpsRedirection();
			app.UseMvc(routeBuilder =>
			{
				routeBuilder.EnableDependencyInjection();
				routeBuilder.Select().Filter().Expand().MaxTop(100).OrderBy().Count();

				routeBuilder.MapODataServiceRoute("odata", "api", odataModelBuilder.GetEdmModel());
			});
		}
	}

	public class AddressAuthorization : IODataAuthorization<Address>
	{
		/// <inheritdoc />
		public IQueryable<Address> BatchApply(IQueryable<Address> source, IODataAuthorizationContext context)
		{
			return source.Where(d => d.AddressId > 500);
		}

		/// <inheritdoc />
		public bool Apply(Address source, IODataAuthorizationContext context)
		{
			return source.AddressId == 600;
		}
	}

	public class AddressWhitelist1 : IODataColumnWhitelist<Address>
	{
		/// <inheritdoc />
		public IEnumerable<string> GetPropertyNames()
		{
			yield return nameof(Address.AddressId);
			yield return nameof(Address.City);
			yield return nameof(Address.ModifiedDate);
		}
	}

	public class AddressBlacklist1 : IODataColumnBlacklist<Address>
	{
		/// <inheritdoc />
		public IEnumerable<string> GetPropertyNames()
		{
			yield return nameof(Address.City);
		}
	}

	public class AddressWhitelist2 : IODataColumnWhitelist<Address>
	{
		/// <inheritdoc />
		public IEnumerable<string> GetPropertyNames()
		{
			yield return nameof(Address.AddressLine1);
		}
	}
}