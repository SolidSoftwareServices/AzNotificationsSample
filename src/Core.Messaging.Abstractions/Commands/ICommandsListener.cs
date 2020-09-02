using System.Threading;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;

namespace Core.Messaging.Abstractions.Commands
{
	public interface ICommandsListener
	{
		Task StartAsync(CancellationToken cancellationToken);
		Task StopAsync(CancellationToken cancellationToken);
	}
		
}