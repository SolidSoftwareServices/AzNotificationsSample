using System;
using System.Threading.Tasks;

namespace Core.Events.Abstractions
{
	public interface IEventsSubscriber<in TEvent, out TMessage> 
	{
		Task SubscribeAsync(TEvent toEvent, Func<TMessage,Task> handler);
	}
}