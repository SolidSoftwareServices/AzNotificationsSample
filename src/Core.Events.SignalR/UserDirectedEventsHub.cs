using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Events;
using Core.Events.SignalR.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Core.Events.SignalR
{
	public abstract class UserDirectedEventsHub<TImplementation> : Hub<CqrsEvent>
		where TImplementation : UserDirectedEventsHub<TImplementation>
	{
		private readonly Lazy<HubConnection> _connection;
		private readonly IConnectionsRepository _connectionsRepository;
		private readonly IHubContext<TImplementation> _hubContext;

		protected UserDirectedEventsHub(IHubContext<TImplementation> hubContext,
			IConnectionsRepository connectionsRepository, Func<Uri> getConnectionUrl)
		{
			_hubContext = hubContext;
			_connectionsRepository = connectionsRepository;

			_connection = new Lazy<HubConnection>(() =>
			{
				return new HubConnectionBuilder().WithUrl(getConnectionUrl(), opts => { }).Build();
			});
		}

		protected async Task _DoSubscribeAsync<TEvent>(string toEvent, Func<TEvent, Task> subscriptionHandler)
			where TEvent : CqrsEvent
		{
			var connection = _connection.Value;
			connection.On(toEvent, subscriptionHandler);
			if (connection.State == HubConnectionState.Disconnected)
			{
				var currentUserPrincipalName = await GetCurrentUserPrincipalName();
				var connectionInfo = new ConnectionInfo {UserId = currentUserPrincipalName};
				connection.Closed += async e => { await _connectionsRepository.Remove(connectionInfo); };

				await connection.StartAsync();
				connectionInfo.ConnectionId = connection.ConnectionId;
				await _connectionsRepository.Add(connectionInfo);
			}
		}

		protected async Task _DoPublishAsync<TEvent>(string toPrincipalName, TEvent message) where TEvent : CqrsEvent
		{
			var connections = await _connectionsRepository.GetAsync(toPrincipalName);
			foreach (var connection in connections)
				await _hubContext.Clients.Client(connection).SendAsync(message.Name, message);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) DisposeConnection().GetAwaiter().GetResult();

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

		protected abstract Task<string> GetCurrentUserPrincipalName();
	}
}