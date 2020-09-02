using System;
using Microsoft.Azure.ServiceBus;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus
{
	class CommandDispatcherSubscription : ICommandDispatcherSubscription
	{
		public Lazy<SubscriptionClient> Client { get; }
		public CommandDispatcherSubscription(IAzureServiceBusCommandsDispatcherSettings settings)
		{
			Client = new Lazy<SubscriptionClient>(() => new SubscriptionClient(settings.CommandDispatcherConnectionString, settings.CommandsDispatcherTopic, settings.CommandDispatcherSubscriptionName));
		}


		public void Dispose()
		{
			if (Client.IsValueCreated)
			{
				Client.Value.CloseAsync().GetAwaiter().GetResult();
			}
		}
	}
}