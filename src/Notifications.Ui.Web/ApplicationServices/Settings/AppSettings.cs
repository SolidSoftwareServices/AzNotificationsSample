using Microsoft.Extensions.Configuration;
using Notifications.Ui.Events;

namespace Notifications.Ui.Web.ApplicationServices
{
	internal class AppSettings : IUiEventsSettings
	{
		private readonly IConfiguration _configuration;

		public AppSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string NotificationEventsUrl => _configuration["Hubs:NotificationEventsUrl"];
	}
}