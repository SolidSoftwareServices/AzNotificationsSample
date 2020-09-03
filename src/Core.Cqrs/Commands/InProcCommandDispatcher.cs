using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.System;
using Microsoft.Extensions.Logging;

namespace Core.Cqrs.Commands
{
	internal class InProcCommandDispatcher : IInProcCommandDispatcher
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<InProcCommandDispatcher> _logger;

		public InProcCommandDispatcher(IServiceProvider serviceProvider,ILogger<InProcCommandDispatcher> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		public async Task ExecuteAsync<TCommand>(TCommand command) where TCommand : CqrsCommand
		{
			await _serviceProvider
				.Resolve<ICommandHandler<TCommand>>()
				.ExecuteAsync(command);
			_logger.LogInformation($"Dispatched command: {command.Id}");
		}

		private static  readonly ConcurrentDictionary<Type,MethodInfo> Cache=new ConcurrentDictionary<Type, MethodInfo>();

		public async Task ExecuteAsync(object command)
		{
			if (!(command is CqrsCommand))
			{
				throw new ArgumentException("Command not valid");
			}

			var type = command.GetType();

			var methodInfo = Cache.GetOrAdd(type, (k) =>

				typeof(InProcCommandDispatcher)
					.GetMethods()
					.Single(x =>
					{
						var genericArguments = x.GetGenericArguments();
						return x.Name == nameof(ExecuteAsync) && genericArguments.Any();
					})
					.MakeGenericMethod(type)
			);
			await (Task)methodInfo.Invoke(this, new[] {command});
		}
	}
}