using System;
using Core.Events.Abstractions;
using Microsoft.Azure.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Notifications.Ui.Events.Connections;
using Notifications.Ui.Events.Hubs;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.Events.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddUiEventHubs(this IServiceCollection services)
		{
			services.AddSignalR(opts => { });

			services.AddTransient<IEventsPublisher<NotificationEvent, NotificationInfo>, NotificationEventsHub>();
			services.AddTransient<IEventsSubscriber<NotificationEvent, NotificationInfo>, NotificationEventsHub>();
			services.AddTransient<IConnectionsRepository, InMemoryConnectionsRepository>();
			//so we allow fluent config
			return services;
		}

		
	}

	
}
