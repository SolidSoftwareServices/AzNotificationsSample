using Core.Cqrs.Abstractions.Queries;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Queries
{
	public class NotificationsQuery:IQuery
	{
		public NotificationInfo.NotificationStatus? Status { get; set; }
	}
}
