using System;
using System.Threading.Tasks;
using Core.Events.SignalR.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Core.Events.SignalR
{
	public abstract class UserDirectedEventsHub<TImplementation, T>:Hub<T> 
		where T : class
		where TImplementation: UserDirectedEventsHub<TImplementation,T>
	{
		private readonly IHubContext<TImplementation> _hubContext;
		private readonly IConnectionsRepository _connectionsRepository;
		private readonly Lazy<HubConnection> _connection;

		protected UserDirectedEventsHub(IHubContext<TImplementation> hubContext,
			IConnectionsRepository connectionsRepository, Func<Uri> getConnectionUrl)
		{
			_hubContext = hubContext;
			_connectionsRepository = connectionsRepository;

			_connection = new Lazy<HubConnection>(() =>
			{ 
				return new HubConnectionBuilder().WithUrl(getConnectionUrl(), opts =>
				{
				}).Build();
			});
		}

		protected async Task _DoSubscribeAsync(string toEvent,Func<T,Task> subscriptionHandler)
		{
			var connection = _connection.Value;
			connection.On(toEvent, subscriptionHandler);
			if (connection.State == HubConnectionState.Disconnected)
			{
				var currentUserPrincipalName = await GetCurrentUserPrincipalName();
				var connectionInfo = new ConnectionInfo { UserId = currentUserPrincipalName};
				connection.Closed += async e =>
				{
					await _connectionsRepository.Remove(connectionInfo);
				};

				await connection.StartAsync();
				connectionInfo.ConnectionId = connection.ConnectionId;
				await _connectionsRepository.Add(connectionInfo);
			}
		}
		public async Task _DoPublishAsync(string toPrincipalName, string @event, T message)
		{

			var connections = await _connectionsRepository.GetAsync(toPrincipalName);
			foreach (var connection in connections)
			{

				await _hubContext.Clients.Client(connection).SendAsync(@event, message);
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

		protected abstract Task<string> GetCurrentUserPrincipalName();
	}
}
