using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Core.Events.SignalR;
using Core.Events.SignalR.Connections;
using Core.Messaging.Abstractions.Events;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Notifications.Ui.DomainModels.Events;
using Notifications.Ui.DomainServices.Users.Queries;

namespace Notifications.Ui.Events.Hubs
{
	internal class NotificationEventsHub : UserDirectedEventsHub<NotificationEventsHub>,
		IEventsPublisher<NotificationEvent>,
		IEventsSubscriber<NotificationEvent>
	{
		private readonly IQueryResolver _queryResolver;


		public NotificationEventsHub(NavigationManager navigationManager,
			IHubContext<NotificationEventsHub> hubContext, IConnectionsRepository connectionsRepository,
			IQueryResolver queryResolver, IUiEventsSettings settings)
			: base(hubContext,
				connectionsRepository,
				() => navigationManager.ToAbsoluteUri(settings.NotificationEventsUrl)
			)
		{
			_queryResolver = queryResolver;
		}


		public async Task PublishAsync(NotificationEvent notificationEvent)
		{
			await _DoPublishAsync(notificationEvent.Notification.To.PrincipalName, notificationEvent);
		}

		public async Task SubscribeAsync(string eventName,Func<NotificationEvent, Task> receptionHandler) 
		{
			await _DoSubscribeAsync(eventName, receptionHandler);
		}

		
		//TODO: CREATE SERVICE
		protected override async Task<string> GetCurrentUserPrincipalName()
		{
			var me = await _queryResolver.GetCurrentUser();
			return me.PrincipalName;
		}

		
	}
}