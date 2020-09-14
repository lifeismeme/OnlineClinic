using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class ServiceBusTest
	{
		private static IConfigurationRoot GetConfig()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("appsettings.json");
			return builder.Build();
		}

		
		[Fact]
		public void Send_NoError()
		{
			//const string data = "!hello -s d@ewe ";
			//string received = null;
			//object ex = null;
			//var sb = new ServiceBus(GetConfig());
			//sb.SetHandler((msg, token) =>
			//{
			//	received = Encoding.UTF8.GetString(msg.Body);
			//}, (exArg) =>
			//{
			//	ex = exArg; return new Task(() => Console.WriteLine());
			//});

			//sb.Send(data);

			//Assert.True(ex == null);
			//Assert.NotNull(received);
			//Assert.Equal(data, received);
		}


	}
}
