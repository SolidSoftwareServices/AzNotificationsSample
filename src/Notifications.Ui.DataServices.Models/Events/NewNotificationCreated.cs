namespace Notifications.Ui.DomainModels.Events
{
	public class NewNotificationCreated : NotificationEvent
	{
		public override string Name { get; } = NotificationEventType.NewNotificationCreated.ToString();
	}
}