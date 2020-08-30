using System;
using Core.Cqrs.Abstractions.Commands;
using Core.Cqrs.Abstractions.Queries;
using Core.Cqrs.Commands;
using Core.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Cqrs
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection AddInProcCqrsDispatchers(this IServiceCollection services)
		{
			services.AddTransient<ICommandDispatcher, InProcCommandDispatcher>();
			services.AddTransient<IQueryResolver, InProcQueryResolver>();
			return services;
		}
	}
}
