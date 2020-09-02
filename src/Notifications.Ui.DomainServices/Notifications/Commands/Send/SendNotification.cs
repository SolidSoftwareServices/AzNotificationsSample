using Core.Cqrs.Abstractions.Commands;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.Send
{
	internal class SendNotification : CqrsCommand
	{
		public NotificationInfo Notification { get; set; }
	}
}