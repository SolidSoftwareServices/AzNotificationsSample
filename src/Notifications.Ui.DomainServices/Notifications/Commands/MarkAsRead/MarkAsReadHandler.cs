using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Commands;
using Core.Cqrs.Abstractions.Events;
using Core.Messaging.Abstractions.Events;
using Notifications.Ui.DataServices.Notifications;
using Notifications.Ui.DomainModels;
using Notifications.Ui.DomainModels.Events;

namespace Notifications.Ui.DomainServices.Notifications.Commands.MarkAsRead
{
	internal class MarkAsReadHandler : ICommandHandler<MarkAsRead>
	{
		private readonly IEventsPublisher<NotificationEvent> _eventsPublisher;
		private readonly INotificationsRepository _repository;

		public MarkAsReadHandler(IEventsPublisher<NotificationEvent> eventsPublisher, INotificationsRepository repository)
		{
			_eventsPublisher = eventsPublisher;
			_repository = repository;
		}


		public async Task ExecuteAsync(MarkAsRead command)
		{
			var msg = await _repository.GetById(command.Notification.Id);
			msg.Status = NotificationInfo.NotificationStatus.ReadCompleted;

			await _eventsPublisher.PublishAsync(new NotificationWasMarkedAsRead {Notification = command.Notification});
		}
	}
}