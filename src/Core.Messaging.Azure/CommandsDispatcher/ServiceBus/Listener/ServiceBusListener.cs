using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Messaging.Abstractions.Commands;

namespace Core.Messaging.Azure.CommandsDispatcher.ServiceBus
{
	class ServiceBusListener:ICommandsListener
	{
		private ICommandDispatcherSubscription _subscription;

		public ServiceBusListener(ICommandDispatcherSubscription subscription)
		{
			_subscription = subscription;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}