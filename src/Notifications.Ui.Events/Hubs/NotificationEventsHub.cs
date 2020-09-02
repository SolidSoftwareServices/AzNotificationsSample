using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Core.Events.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainServices.Users.Queries;
using Notifications.Ui.Events.Connections;
using ConnectionInfo = Notifications.Ui.Events.Connections.ConnectionInfo;

namespace Notifications.Ui.Events.Hubs
{
	internal class NotificationEventsHub : Hub<NotificationInfo>, 
		IEventsPublisher<NotificationEvent,NotificationInfo>,
		IEventsSubscriber<NotificationEvent,NotificationInfo>
	{
		
		private readonly IHubContext<NotificationEventsHub> _hubContext;
		private readonly IConnectionsRepository _connectionsRepository;
		private readonly IQueryResolver _queryResolver;
		private readonly Lazy<HubConnection> _connection;

		public NotificationEventsHub(IConfiguration configuration, NavigationManager navigationManager,
			IHubContext<NotificationEventsHub> hubContext, IConnectionsRepository connectionsRepository,
			 IQueryResolver queryResolver, IUiEventsSettings settings)
		{
			_hubContext = hubContext;
			_connectionsRepository = connectionsRepository;
			_queryResolver = queryResolver;
			_connection = new Lazy<HubConnection>(() =>
			{
				var url = navigationManager.ToAbsoluteUri(settings.NotificationEventsUrl);
				return new HubConnectionBuilder().WithUrl(url, opts =>
				{
				}).Build();
			});
		}


		public async Task SubscribeAsync(NotificationEvent toEvent, Func<NotificationInfo,Task> handler)
		{
			var connection = _connection.Value;
			connection.On(toEvent.ToString(), handler);
			if (connection.State == HubConnectionState.Disconnected)
			{
				var me = await _queryResolver.GetCurrentUser();
				var connectionInfo = new ConnectionInfo { UserId = me.PrincipalName};
				connection.Closed += async e =>
				{
					await _connectionsRepository.Remove(connectionInfo);
				};

				await connection.StartAsync();
				connectionInfo.ConnectionId = connection.ConnectionId;
				await _connectionsRepository.Add(connectionInfo);
			}
		}


		public async Task PublishAsync(NotificationEvent @event, NotificationInfo notification)
		{
			
			var connections=await _connectionsRepository.GetAsync(notification.To.PrincipalName);
			foreach (var connection in connections)
			{

				await _hubContext.Clients.Client(connection).SendAsync(@event.ToString(), notification);
			}
			
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				DisposeConnection().GetAwaiter().GetResult();
			}

			base.Dispose(disposing);

			async Task DisposeConnection()
			{
				if (_connection.IsValueCreated)
				{
					var connection = _connection.Value;
					if (connection.State != HubConnectionState.Disconnected)
						await connection.StopAsync();
					await connection.DisposeAsync();
				}
			}
		}


		
	}
}