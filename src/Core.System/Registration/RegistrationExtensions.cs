using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.System
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection RegisterAssemblyTypesAsTransient(this IServiceCollection services,
			Assembly assembly, Func<Type, bool> filterFunc = null)
		{
			var implementations = assembly.GetTypes().Where(x =>
				x.IsClass && !x.IsAbstract && x.Namespace != null
				&& !x.Name.StartsWith("<"));
			if (filterFunc != null) implementations = implementations.Where(filterFunc);
			foreach (var svc in implementations)
			{
				var interfaces = svc.GetInterfaces() ?? new Type[0];
				foreach (var contract in interfaces) services.AddTransient(contract, svc);
			}

			return services;
		}

		public static IServiceCollection RegisterAsImplementedInterfacesTransient<TImplementation>(
			this IServiceCollection services)
		{
			var allInterfaces = new List<Type>();
			var current = typeof(TImplementation);

			do
			{
				var interfaces = current.GetInterfaces();
				if (interfaces.Any()) allInterfaces.AddRange(interfaces);
			} while ((current = current.BaseType) != null);

			foreach (var @interface in allInterfaces.Distinct())
				services.AddTransient(@interface, typeof(TImplementation));

			return services;
		}
	}
}