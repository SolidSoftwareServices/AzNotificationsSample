using Core.Cqrs.Abstractions.Queries;

namespace Notifications.Ui.DomainServices.Users.Queries
{
	public class UsersQuery:IQuery
	{
		public bool OnlyMe { get; set; }
	}
}
