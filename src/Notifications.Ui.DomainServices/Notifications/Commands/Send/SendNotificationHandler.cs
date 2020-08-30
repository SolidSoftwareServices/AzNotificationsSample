using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.Events.Abstractions;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.Send
{
	class SendNotificationHandler : ICommandHandler<SendNotification>
	{
		private readonly IEventsPublisher<NotificationEvent, NotificationInfo> _eventsPublisher;
		private readonly INotificationsRepository _repository;
		public SendNotificationHandler(IEventsPublisher<NotificationEvent, NotificationInfo> eventsPublisher, INotificationsRepository repository)
		{
			_eventsPublisher = eventsPublisher;
			_repository = repository;
		}

		public async Task ExecuteAsync(SendNotification command)
		{
			command.Notification.Status = NotificationInfo.NotificationStatus.New;
			await _repository.Add(command.Notification);
			command.Notification.Status = NotificationInfo.NotificationStatus.Sent;
			await _eventsPublisher.PublishAsync(NotificationEvent.NewNotificationReceived, command.Notification);
		}
	}
}