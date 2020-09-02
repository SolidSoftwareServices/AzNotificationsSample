using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Events.SignalR.Connections
{
	 public interface IConnectionsRepository
	{
		Task Add(ConnectionInfo connection);
		Task Remove(ConnectionInfo connection);
		Task<IEnumerable<string>> GetAsync(string userId);
	}
}