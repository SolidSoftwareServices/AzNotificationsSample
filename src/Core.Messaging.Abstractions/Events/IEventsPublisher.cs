using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Events;

namespace Core.Messaging.Abstractions.Events
{
	public interface IEventsPublisher<in TMessage> where TMessage : CqrsEvent
	{
		Task PublishAsync(TMessage notification);
	}
}