using Core.System;
using Microsoft.Extensions.DependencyInjection;

namespace Notifications.Ui.Web.ApplicationServices.Settings
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddAppSettings(this IServiceCollection services)
		{
			return services.RegisterAsImplementedInterfacesTransient<AppSettings>();
		}
	}
}