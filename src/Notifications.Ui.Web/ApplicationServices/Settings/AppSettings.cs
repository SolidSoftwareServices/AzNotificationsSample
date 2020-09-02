using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Notifications.Ui.Events;
using Notifications.Ui.Events.Hubs;

namespace Notifications.Ui.Web.ApplicationServices
{
	class AppSettings:IUiEventsSettings
	{

		private readonly IConfiguration _configuration;

		public AppSettings(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string NotificationEventsUrl => _configuration["Hubs:NotificationEventsUrl"];
	}
}
