namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus
{
	public interface IAzureServiceBusCommandsDispatcherSettings
	{
		string CommandDispatcherConnectionString { get; }
		string CommandsDispatcherTopic { get; }
		string CommandDispatcherSubscriptionName { get; }
	}
}