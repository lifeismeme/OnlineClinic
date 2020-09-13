using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace OnlineClinic.Repositories
{
	public class ServiceBus
	{
		private readonly string connectionString;
		public readonly string queueName;

		public ServiceBus()
		{
			var config = Program.GetConfig();
			connectionString = config["Azure:ServiceBus:ConnectionString"];
			queueName = config["Azure:ServiceBus:QueueName"];
		}

		public ServiceBus(IConfigurationRoot config)
		{
			connectionString = config["Azure:ServiceBus:ConnectionString"];
			queueName = config["Azure:ServiceBus:QueueName"];
		}
		
		public void Send(byte[] data)
		{ 
			var message = new Message(data);

			new QueueClient(connectionString, queueName).SendAsync(message);
		}

		public async void SetHandler(Action<Message, CancellationToken> handler, Func<ExceptionReceivedEventArgs, Task> errorHandler)
		{
			await Task.Factory.StartNew(() =>
			{
				var queueClient = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);
				var options = new MessageHandlerOptions(errorHandler)
				{
					MaxConcurrentCalls = 1,
					AutoComplete = false
				};
				queueClient.RegisterMessageHandler(async (Msg, cancelToken) => {
					try
					{
						handler.Invoke(Msg, cancelToken);
					}
					finally
					{
						await queueClient.CompleteAsync(Msg.SystemProperties.LockToken);
					}
				}, options);
			});

		}

		public static Action<Message, CancellationToken> DefaultMessageHandler()
		{
			return new Action<Message, CancellationToken>((Msg, cancelToken) =>
			{
				Console.WriteLine($"- SequenceNumber: {Msg.SystemProperties.SequenceNumber}");
				Console.WriteLine($"  - Body:{Encoding.UTF8.GetString(Msg.Body)} ");

				Debug.WriteLine($"- SequenceNumber: {Msg.SystemProperties.SequenceNumber}");
				Debug.WriteLine($"  - Body:{Encoding.UTF8.GetString(Msg.Body)} ");
			});
		}

		public static Func<ExceptionReceivedEventArgs, Task> DefaultErrorHandler()
		{
			return new Func<ExceptionReceivedEventArgs, Task>(arg =>
			{
				return Task.Run(() =>
				{
					Console.Error.WriteLine(arg.Exception.ToString());
					Console.Error.WriteLine($"- Error: {arg.Exception.Message}");

					Debug.WriteLine(arg.Exception.ToString());
					Debug.WriteLine($"- Error: {arg.Exception.Message}");
				});
			});
		}

	}
}
