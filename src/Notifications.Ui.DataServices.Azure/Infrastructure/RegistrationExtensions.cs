using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Ui.DataServices.Azure.Notifications;
using Notifications.Ui.DataServices.Notifications;

namespace Notifications.Ui.DataServices.Azure.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddAzureNotificationDataServices(this IServiceCollection services)
		{
			services.AddSingleton<INotificationsRepository, NotificationRepository>();
			//so we allow fluent config
			return services;
		}
	}
}
