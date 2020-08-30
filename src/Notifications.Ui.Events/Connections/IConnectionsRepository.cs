using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifications.Ui.Events.Connections
{
	 interface IConnectionsRepository
	{
		Task Add(ConnectionInfo connection);
		Task Remove(ConnectionInfo connection);
		Task<IEnumerable<string>> GetAsync(string userId);
	}
}