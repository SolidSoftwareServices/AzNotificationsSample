using System;

namespace Core.System
{
	public static class ServiceProviderExtensions
	{
		public static TResult Resolve<TResult>(this IServiceProvider serviceProvider)
		{
			return (TResult) serviceProvider.GetService(typeof(TResult));
		}
	}
}