using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.Send
{
	public static class QuerySyntaxHelper
	{
		public static async Task SendNotification(this ICommandDispatcher dispatcher, NotificationInfo notification)
		{
			await dispatcher.Execute(new SendNotification { Notification = notification });
		}
	}
}