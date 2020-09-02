using Core.Events.SignalR.Connections;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Events.SignalR.Infrastructure
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddCoreSignalR(this IServiceCollection services)
		{
			services.AddSignalR(opts => { });

			services.AddTransient<IConnectionsRepository, InMemoryConnectionsRepository>();
			//so we allow fluent config
			return services;
		}
	}
}