using System;
using Microsoft.Azure.ServiceBus;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus
{
	interface ICommandDispatcherSubscription
	{
		Lazy<SubscriptionClient> Client { get; }
	}
}