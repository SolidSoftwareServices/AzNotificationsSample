using Core.Messaging.Azure.CommandsDispatcher.ServiceBus;
using Microsoft.Extensions.Configuration;
using Notifications.Ui.Events;

namespace Notifications.Ui.Web.ApplicationServices.Settings
{
	internal class AppSettings 
			: IUiEventsSettings, 
			  IAzureServiceBusCommandsDispatcherSettings
	{
		private readonly IConfiguration _configuration;

		public AppSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string NotificationEventsUrl => _configuration["Hubs:NotificationEventsUrl"];
		public string CommandDispatcherConnectionString => _configuration["AzureCommandsDispatcher:ConnectionString"];
		public string CommandsDispatcherTopic => _configuration["AzureCommandsDispatcher:Topic"];
		public string CommandDispatcherSubscriptionName { get; }
	}
}