using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Microsoft.Graph;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Users.Queries
{
	internal class UsersQueryHandler : IQueryHandler<UsersQuery, IEnumerable<UserInfo>>
	{
		private readonly Lazy<GraphServiceClient> _graphClient;

		public UsersQueryHandler(IAuthenticationProvider authenticationProvider)
		{
			_graphClient = new Lazy<GraphServiceClient>(() => new GraphServiceClient(authenticationProvider));
		}

		public async Task<IEnumerable<UserInfo>> ExecuteAsync(UsersQuery query)
		{
			IEnumerable<User> dtos;
			if (query.OnlyMe)
			{
				var me = await _graphClient.Value.Me.Request().GetAsync();
				dtos = new[] {me};
			}
			else
			{
				dtos = (await _graphClient.Value.Groups["892f93be-cc4e-4d9e-b753-46f9e827b609"]
					.Members
					.Request()
					.GetAsync()).Cast<User>();

				//TODO: paging user results 
			}


			return dtos
				.Select(Map);
			;
		}

		private UserInfo Map(User user)
		{
			return new UserInfo {PrincipalName = user.UserPrincipalName, DisplayName = user.DisplayName};
		}
	}
}