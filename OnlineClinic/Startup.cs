using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Models;
using Microsoft.Extensions.Azure;
using Azure.Storage.Queues;
using Azure.Storage.Blobs;
using Azure.Core.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace OnlineClinic
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//DB from the Identity for login
			services.AddDbContext<OnlineClinicContext>(options =>
					options.UseSqlServer(
						Configuration.GetConnectionString("OnlineClinicContextConnection")));

			services.AddDefaultIdentity<User>()
				.AddEntityFrameworkStores<OnlineClinicContext>();
			services.AddAzureClients(builder =>
			{
				builder.AddBlobServiceClient(Configuration["ConnectionStrings:DefaultEndpointsProtocol=https;AccountName=tp040971;AccountKey=RGsb3EA7VprieSCnNqO8xJpJlfSlENDi/7+qWt84FgsCO+PXDRhlQdTgmVAgYg/58+pEs51A5AzSfa8IaTfKBw==;EndpointSuffix=core.windows.net:blob"], preferMsi: true);
				builder.AddQueueServiceClient(Configuration["ConnectionStrings:DefaultEndpointsProtocol=https;AccountName=tp040971;AccountKey=RGsb3EA7VprieSCnNqO8xJpJlfSlENDi/7+qWt84FgsCO+PXDRhlQdTgmVAgYg/58+pEs51A5AzSfa8IaTfKBw==;EndpointSuffix=core.windows.net:queue"], preferMsi: true);
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
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseCookiePolicy();
			app.UseAuthentication();


			//Localization
			//Set system native culture (lang and currency)
			var supportedCultures = new[]
			{
				new CultureInfo("en-my"), //English Malaysia, use RM instead of $
            };
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-my"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});
			//


			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
	internal static class StartupExtensions
	{
		public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
		{
			if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
			{
				return builder.AddBlobServiceClient(serviceUri);
			}
			else
			{
				return builder.AddBlobServiceClient(serviceUriOrConnectionString);
			}
		}
		public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
		{
			if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri serviceUri))
			{
				return builder.AddQueueServiceClient(serviceUri);
			}
			else
			{
				return builder.AddQueueServiceClient(serviceUriOrConnectionString);
			}
		}
	}
}
