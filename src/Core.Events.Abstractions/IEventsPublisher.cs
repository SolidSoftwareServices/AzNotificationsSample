using System.Threading.Tasks;

namespace Core.Events.Abstractions
{
	public interface IEventsPublisher<in TEvent, in TMessage> 
	{
		Task PublishAsync(TEvent @event, TMessage notification);
	}
}