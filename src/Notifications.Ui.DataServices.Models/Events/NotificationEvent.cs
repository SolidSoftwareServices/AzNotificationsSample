using Core.Cqrs.Abstractions.Events;

namespace Notifications.Ui.DomainModels.Events
{
	public class NotificationEvent : CqrsEvent
	{
		public NotificationInfo Notification { get; set; }
		public override string Name { get; }
	}
}