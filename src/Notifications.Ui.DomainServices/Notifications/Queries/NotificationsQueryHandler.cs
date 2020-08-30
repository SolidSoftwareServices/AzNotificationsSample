using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Core.Events.Abstractions;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainServices.Users.Queries;

namespace Notifications.Ui.DomainServices.Notifications.Queries
{
	class NotificationsQueryHandler : IQueryHandler<NotificationsQuery, IEnumerable<NotificationInfo>>
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

			return await _repository.GetInbox(me,query.Status);
			
		}
	}
}