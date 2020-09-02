using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Events;

namespace Core.Messaging.Abstractions.Events
{
	public interface IEventsSubscriber<out TEvent> where TEvent : CqrsEvent
	{
		Task SubscribeAsync(string eventName,Func<TEvent, Task> receptionHandler) ;
	}
}