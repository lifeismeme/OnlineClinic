using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using OnlineClinic.Models;
using OnlineClinic.Repositories;
using Xunit;


namespace OnlineClinicUnitTest
{
	public class ServiceBus
	{

		const string ServiceBusConnectionString = "Endpoint=sb://tp040971.servicebus.windows.net/;SharedAccessKeyName=server;SharedAccessKey=+/eoa/slh6Cfs4bb41j4GyW/2426GGyJroA7X+DCQJE=";
		const string QueueName = "appointments";

		[Fact]
		public void Send_NoError()
		{
			var queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
			string data = $"fffffffffffffffffffffffFFFffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffMessage";
			var message = new Message(Encoding.UTF8.GetBytes(data));

			queueClient.SendAsync(message);

		}

		[Fact]
		public void RegisterHandler()
		{
			Task.Factory.StartNew(() =>
			{
				var queueClient = new QueueClient(ServiceBusConnectionString, QueueName, ReceiveMode.PeekLock);
				var options = new MessageHandlerOptions(arg =>
				{
					return Task.Run(() =>
					{
						Console.WriteLine(arg.Exception.ToString());
						Console.WriteLine($"Error {arg.Exception.Message}");
					}
					);
				})
				{
					MaxConcurrentCalls = 1,
					AutoComplete = false
				};
				queueClient.RegisterMessageHandler(ExecuteMessageProcessing, options);
			});

		}

		//Part 2: Received Message from the Service Bus - get data step
		private static async Task ExecuteMessageProcessing(Message message, CancellationToken cancelToken)
		{
			var queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

			Console.WriteLine($"SequenceNumber: {message.SystemProperties.SequenceNumber} \nBody:{Encoding.UTF8.GetString(message.Body)}");

			await queueClient.CompleteAsync(message.SystemProperties.LockToken);
		}

	}
}
