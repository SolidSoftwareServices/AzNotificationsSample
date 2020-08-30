using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.System;

namespace Core.Cqrs.Commands
{
	class InProcCommandDispatcher:ICommandDispatcher
	{

		private readonly IServiceProvider _serviceProvider;

		public InProcCommandDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task Execute<TCommand>(TCommand command) where TCommand : ICommand
		{
			var handlers = _serviceProvider
				.Resolve<IEnumerable<ICommandHandler<TCommand>>>()
				.Select(h=>h.ExecuteAsync(command));
			await Task.WhenAll(handlers);

		}
	}
}
