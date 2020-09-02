using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task Execute<TCommand>(TCommand command) where TCommand : CqrsCommand
		{
			await _serviceProvider
				.Resolve<ICommandHandler<TCommand>>()
				.ExecuteAsync(command);
			_logger.LogInformation($"Dispatched command: {command.Id}");
		}
	}
}