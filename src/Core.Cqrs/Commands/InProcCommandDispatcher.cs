using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.System;

namespace Core.Cqrs.Commands
{
	internal class InProcCommandDispatcher : ICommandDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public InProcCommandDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task Execute<TCommand>(TCommand command) where TCommand : ICommand
		{
			await _serviceProvider
				.Resolve<ICommandHandler<TCommand>>()
				.ExecuteAsync(command);
		}
	}
}