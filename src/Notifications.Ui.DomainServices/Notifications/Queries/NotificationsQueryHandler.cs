using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainServices.Users.Queries;

namespace Notifications.Ui.DomainServices.Notifications.Queries
{
	internal class NotificationsQueryHandler : IQueryHandler<NotificationsQuery, IEnumerable<NotificationInfo>>
	{
		private readonly IQueryResolver _queryResolver;
		private readonly INotificationsRepository _repository;

		public NotificationsQueryHandler(IQueryResolver queryResolver, INotificationsRepository repository)
		{
			_queryResolver = queryResolver;
			_repository = repository;
		}


		public async Task<IEnumerable<NotificationInfo>> ExecuteAsync(NotificationsQuery query)
		{
			var me = await _queryResolver.GetCurrentUser();

			return await _repository.GetInbox(me, query.Status);
		}
	}
}