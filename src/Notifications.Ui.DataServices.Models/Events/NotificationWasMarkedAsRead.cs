namespace Notifications.Ui.DomainModels.Events
{
	public class NotificationWasMarkedAsRead : NotificationEvent
	{
		public override string Name { get; } = NotificationEventType.NotificationWasRead.ToString();
	}
}