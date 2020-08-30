using Core.System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Notifications.Ui.DataServices.Notifications;

namespace Notifications.Ui.DomainServices.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddDomainServices(this IServiceCollection services)
		{
			services.RegisterAssemblyTypesAsTransient(typeof(RegistrationExtensions).Assembly);
			//so we allow fluent config
			return services;
		}
	}
}
