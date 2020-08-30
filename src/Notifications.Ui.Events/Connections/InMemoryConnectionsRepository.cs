using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Ui.Events.Connections
{
	class InMemoryConnectionsRepository : IConnectionsRepository
	{
		public static ConcurrentDictionary<string, HashSet<string>> ConnectionsDataSource = new ConcurrentDictionary<string, HashSet<string>>();
		public  Task Add(ConnectionInfo connection)
		{
			var userConnections =
				 EnsureCollection(connection.UserId);
			userConnections.Add(connection.ConnectionId);
			return Task.CompletedTask;
		}

		

		public Task Remove(ConnectionInfo connection)
		{
			EnsureCollection(connection.UserId).Remove(connection.ConnectionId);
			return Task.CompletedTask;
		}

		public async Task<IEnumerable<string>> GetAsync(string userId)
		{
			return EnsureCollection(userId);
		}

		private  HashSet<string> EnsureCollection(string userId)
		{
			return ConnectionsDataSource.GetOrAdd(userId, (key) => new HashSet<string>());
		}
	}
}