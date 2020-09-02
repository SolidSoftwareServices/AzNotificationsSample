using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Messaging.Abstractions.Commands;
using Microsoft.Extensions.Hosting;

namespace Notifications.Ui.Web.ApplicationServices.HostedServices
{
	public class CommandsListener:IHostedService
	{
		private readonly ICommandsListener _decoratedListener;

		public CommandsListener(ICommandsListener decoratedListener)
		{
			_decoratedListener = decoratedListener;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			return _decoratedListener.StartAsync(cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return _decoratedListener.StopAsync(cancellationToken);
		}
	}
}
