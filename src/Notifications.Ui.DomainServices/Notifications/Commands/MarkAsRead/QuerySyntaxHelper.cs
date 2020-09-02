using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.MarkAsRead
{
	public static class QuerySyntaxHelper
	{
		public static async Task MarkNotificationAsRead(this ICommandDispatcher dispatcher,
			NotificationInfo notification)
		{
			await dispatcher.Execute(new MarkAsRead {Notification = notification});
		}
	}
}