using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OnlineClinic
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				CreateWebHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
				Console.Error.WriteLine(ex.Message);
				throw;
			}
		}

		public static IConfigurationRoot GetConfig()
		{
			try
			{
				var builder = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("appsettings.json");
				return builder.Build();
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
				Console.Error.WriteLine(ex.Message);
				throw;
			}

		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
