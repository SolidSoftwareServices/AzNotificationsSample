using Core.Cqrs.Abstractions.Commands;
using Core.Cqrs.Abstractions.Queries;
using Core.Cqrs.Commands;
using Core.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Cqrs
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddInProcCommandDispatcher(this IServiceCollection services)
		{
			services.AddTransient<ICommandDispatcher, InProcCommandDispatcher>();
			return services;
		}

		public static IServiceCollection AddInProcQueryResolver(this IServiceCollection services)
		{
			services.AddTransient<IQueryResolver, InProcQueryResolver>();
			return services;
		}
	}
}