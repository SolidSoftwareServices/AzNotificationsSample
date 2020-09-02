using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;

namespace Core.Messaging.Abstractions.Commands
{
	public interface ICommandsSubscriber<in TEvent, out TMessage> where TMessage : ICommand
	{
		Task SubscribeAsync(TEvent toEvent, Func<TMessage, Task> handler);
	}
}