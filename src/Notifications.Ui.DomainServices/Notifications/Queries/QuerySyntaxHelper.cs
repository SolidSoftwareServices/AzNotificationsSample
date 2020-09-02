using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Queries
{
	public static class QuerySyntaxHelper
	{
		public static async Task<IEnumerable<NotificationInfo>> GetNonReadNotifications(this IQueryResolver resolver)
		{
			return await resolver.ExecuteAsync<NotificationsQuery, IEnumerable<NotificationInfo>>(new NotificationsQuery
				{Status = NotificationInfo.NotificationStatus.Sent});
		}
	}
}