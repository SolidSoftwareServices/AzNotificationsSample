using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;

namespace Core.Messaging.Abstractions.Commands
{
	public interface ICommandsPublisher<in TEvent, in TMessage> where TMessage : ICommand
	{
		Task PublishAsync(TEvent @event, TMessage notification);
	}
}