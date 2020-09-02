using Core.Cqrs.Abstractions.Commands;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.MarkAsRead
{
	internal class MarkAsRead : CqrsCommand
	{
		public NotificationInfo Notification { get; set; }
	}
}