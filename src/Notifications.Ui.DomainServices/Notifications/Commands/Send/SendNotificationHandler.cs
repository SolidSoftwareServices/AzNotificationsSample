using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.Cqrs.Abstractions.Events;
using Core.Messaging.Abstractions.Events;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainModels.Events;

namespace Notifications.Ui.DomainServices.Notifications.Commands.Send
{
	internal class SendNotificationHandler : ICommandHandler<SendNotification>
	{
		private readonly IEventsPublisher<NotificationEvent> _eventsPublisher;
		private readonly INotificationsRepository _repository;

		public SendNotificationHandler(IEventsPublisher<NotificationEvent> eventsPublisher, INotificationsRepository repository)
		{
			_eventsPublisher = eventsPublisher;
			_repository = repository;
		}


		public async Task ExecuteAsync(SendNotification command)
		{
			command.Notification.Status = NotificationInfo.NotificationStatus.New;
			await _repository.Add(command.Notification);
			command.Notification.Status = NotificationInfo.NotificationStatus.Sent;
			await _eventsPublisher.PublishAsync(new NewNotificationCreated {Notification = command.Notification});
		}
	}
}