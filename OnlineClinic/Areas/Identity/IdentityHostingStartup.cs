using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineClinic.Areas.Identity.Data;
using OnlineClinic.Models;

[assembly: HostingStartup(typeof(OnlineClinic.Areas.Identity.IdentityHostingStartup))]
namespace OnlineClinic.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*
             * Moved to Startup.cs
             */

            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<OnlineClinicContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("OnlineClinicContextConnection")));

            //    services.AddDefaultIdentity<User>()
            //        .AddEntityFrameworkStores<OnlineClinicContext>();
            //});
        }
    }
}