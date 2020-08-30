using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DataServices.Notifications
{
	public interface INotificationsRepository
	{
		Task Add(NotificationInfo notification);
		Task<IEnumerable<NotificationInfo>> GetAll();
		Task Update(NotificationInfo notification);
		Task<NotificationInfo> GetById(Guid notificationId);
		Task<IEnumerable<NotificationInfo>> GetInbox(UserInfo user, NotificationInfo.NotificationStatus? status);
	}
}