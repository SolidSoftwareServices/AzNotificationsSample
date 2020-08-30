using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Users.Queries
{
	public static class QuerySyntaxHelper
	{
		public static async Task<UserInfo> GetCurrentUser(this IQueryResolver resolver)
		{
			return (await resolver.ExecuteAsync<UsersQuery, IEnumerable<UserInfo>>(new UsersQuery() { OnlyMe = true })).SingleOrDefault();
		}
		public static async Task<IEnumerable<UserInfo>> GetAllUsers(this IQueryResolver resolver)
		{
			return (await resolver.ExecuteAsync<UsersQuery, IEnumerable<UserInfo>>(new UsersQuery() ));
		}
	}
}