using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Core.Events.Abstractions;
using Core.Events.SignalR;
using Core.Events.SignalR.Connections;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainServices.Users.Queries;

namespace Notifications.Ui.Events.Hubs
{
	internal class NotificationEventsHub : UserDirectedEventsHub<NotificationEventsHub, NotificationInfo>,
		IEventsPublisher<NotificationEvent, NotificationInfo>,
		IEventsSubscriber<NotificationEvent, NotificationInfo>
	{
		private readonly IQueryResolver _queryResolver;


		public NotificationEventsHub(NavigationManager navigationManager,
			IHubContext<NotificationEventsHub> hubContext, IConnectionsRepository connectionsRepository,
			IQueryResolver queryResolver, IUiEventsSettings settings)
			: base(hubContext,
				connectionsRepository,
				()=>navigationManager.ToAbsoluteUri(settings.NotificationEventsUrl)
			)
		{
			_queryResolver = queryResolver;
		}


		public async Task PublishAsync(NotificationEvent @event, NotificationInfo notification)
		{
			await _DoPublishAsync(notification.To.PrincipalName, @event.ToString(), notification);
		}

		public async Task SubscribeAsync(NotificationEvent toEvent, Func<NotificationInfo, Task> handler)
		{
			await _DoSubscribeAsync(toEvent.ToString(), handler);
		}

		//TODO: CREATE SERVICE
		protected override async Task<string> GetCurrentUserPrincipalName()
		{
			var me = await _queryResolver.GetCurrentUser();
			return me.PrincipalName;
		}
	}
}