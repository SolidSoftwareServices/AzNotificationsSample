using Core.Events.Abstractions;
using Core.Events.SignalR.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Ui.DomainModels;
using Notifications.Ui.Events.Hubs;

namespace Notifications.Ui.Events.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddUiEventHubs(this IServiceCollection services)
		{
			services.AddCoreSignalR();

			services.AddTransient<IEventsPublisher<NotificationEvent, NotificationInfo>, NotificationEventsHub>();
			services.AddTransient<IEventsSubscriber<NotificationEvent, NotificationInfo>, NotificationEventsHub>();

			//so we allow fluent config
			return services;
		}
	}
}