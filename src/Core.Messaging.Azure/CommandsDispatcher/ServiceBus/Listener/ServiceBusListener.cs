using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.Messaging.Abstractions.Commands;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus.Listener
{
	class ServiceBusListener:ICommandsListener
	{
		private readonly ICommandDispatcherSubscription _subscriptionClient;
		private readonly ILogger<ServiceBusListener> _logger;
		private readonly IInProcCommandDispatcher _commandDispatcher;

		public ServiceBusListener(ICommandDispatcherSubscription subscriptionClientClient,ILogger<ServiceBusListener> logger,IInProcCommandDispatcher commandDispatcher)
		{
			_subscriptionClient = subscriptionClientClient;
			_logger = logger;
			_commandDispatcher = commandDispatcher;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
			{
				MaxConcurrentCalls = 1,

				AutoComplete = false
			};

			_subscriptionClient.Client.Value.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);

			return Task.CompletedTask;
			Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
			{
				var sb=new StringBuilder($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
				var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
				sb.AppendLine("Exception context for troubleshooting:");
				sb.AppendLine($"- Endpoint: {context.Endpoint}");
				sb.AppendLine($"- Entity Path: {context.EntityPath}");
				sb.AppendLine($"- Executing Action: {context.Action}");
				return Task.CompletedTask;
			}
		}

		private async Task ProcessMessagesAsync(Message message, CancellationToken ct)
		{

			var command = ServiceBusMessage.From(message).GetCommand();
			_logger.LogInformation($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{command.Id}");

			await _commandDispatcher.ExecuteAsync(command);

			// Complete the message so that it is not received again.
			// This can be done only if the subscriptionClient is created in ReceiveMode.PeekLock mode (which is default).
			await _subscriptionClient.Client.Value.CompleteAsync(message.SystemProperties.LockToken);

		}


		public async Task StopAsync(CancellationToken cancellationToken)
		{
			//TODO:??
		}
	}
}