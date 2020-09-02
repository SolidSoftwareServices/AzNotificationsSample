using Core.Messaging.Azure.CommandsDispatcher.ServiceBus;
using Core.System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Messaging.Azure.Infrastructure
{
	public static class RegistrationExtensions 
	{
		public static IServiceCollection AddAzureServiceBusAsCommandDispatcher(this IServiceCollection services)
		{
			services.AddSingleton<ICommandDispatcherTopic, CommandDispatcherTopic>();
			services.AddSingleton<ICommandDispatcherSubscription, CommandDispatcherSubscription>();

			services.AddImplementedInterfacesAsTransient<ServiceBusCommandDispatcher>();

			return services;
		}

		public static IServiceCollection AddAzureServiceBusAsCommandListener(this IServiceCollection services)
		{
			
			services.AddImplementedInterfacesAsTransient<ServiceBusCommandDispatcher>();

			return services;
		}
	}
}