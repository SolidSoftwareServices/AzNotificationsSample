using Core.Events.SignalR.Infrastructure;
using Core.System;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Ui.Events.Hubs;

namespace Notifications.Ui.Events.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddUiEventHubs(this IServiceCollection services)
		{
			services.AddCoreSignalR();

			services.RegisterAsImplementedInterfacesTransient<NotificationEventsHub>();

			//so we allow fluent config
			return services;
		}
	}
}