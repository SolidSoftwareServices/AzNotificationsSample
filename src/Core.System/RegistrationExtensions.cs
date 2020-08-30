using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.System
{
	public static class RegistrationExtensions
	{
		public static IServiceCollection RegisterAssemblyTypesAsTransient(this IServiceCollection services,Assembly assembly, Func<Type, bool> filterFunc=null)
		{
			
			var implementations = assembly.GetTypes().Where(x =>
				x.IsClass && !x.IsAbstract && x.Namespace != null
				&& !x.Name.StartsWith("<"));
			if (filterFunc != null)
			{
				implementations = implementations.Where(filterFunc);
			}
			foreach (var svc in implementations)
			{
				var interfaces = svc.GetInterfaces() ?? new Type[0];
				foreach (var contract in interfaces)
				{
					services.AddTransient(contract, svc);

				}
			}

			//so we allow fluent config
			return services;
		}
	}
}