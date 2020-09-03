using System;
using Microsoft.Azure.ServiceBus;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus.Listener
{
	interface ICommandDispatcherSubscription
	{
		Lazy<SubscriptionClient> Client { get; }
	}
}