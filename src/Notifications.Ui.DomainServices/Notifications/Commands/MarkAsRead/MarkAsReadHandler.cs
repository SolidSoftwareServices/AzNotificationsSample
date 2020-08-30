using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.Events.Abstractions;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.MarkAsRead
{
	class MarkAsReadHandler : ICommandHandler<MarkAsRead>
	{
		private readonly IEventsPublisher<NotificationEvent, NotificationInfo> _eventsPublisher;
		private  readonly INotificationsRepository _repository;

		public MarkAsReadHandler(Core.Events.Abstractions.IEventsPublisher<NotificationEvent, NotificationInfo> eventsPublisher, INotificationsRepository repository)
		{
			_eventsPublisher = eventsPublisher;
			_repository = repository;
		}


		public async Task ExecuteAsync(Commands.MarkAsRead.MarkAsRead command)
		{
			var msg = await _repository.GetById(command.Notification.Id);
			msg.Status = NotificationInfo.NotificationStatus.ReadCompleted;

			await _eventsPublisher.PublishAsync(NotificationEvent.NotificationWasRead, command.Notification);
		}
	}
}