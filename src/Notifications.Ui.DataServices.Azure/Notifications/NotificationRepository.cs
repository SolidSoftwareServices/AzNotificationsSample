using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DataServices.Azure.Notifications
{
	internal class NotificationRepository : INotificationsRepository
	{
		private readonly Dictionary<Guid, NotificationInfo> _dbCollection = new Dictionary<Guid, NotificationInfo>();

		public async Task Add(NotificationInfo notification)
		{
			_dbCollection.Add(notification.Id, notification);
		}

		public async Task<IEnumerable<NotificationInfo>> GetAll()
		{
			return _dbCollection.Values;
		}

		public async Task Update(NotificationInfo notification)
		{
			_dbCollection[notification.Id] = notification;
		}

		public async Task<NotificationInfo> GetById(Guid notificationId)
		{
			return _dbCollection[notificationId];
		}

	

		public async Task<IEnumerable<NotificationInfo>> GetInbox(UserInfo user, NotificationInfo.NotificationStatus? status)
		{
			return _dbCollection.Values.Where(x => user.Equals(x.To) && (status == null || x.Status == status));
		}
	}
}