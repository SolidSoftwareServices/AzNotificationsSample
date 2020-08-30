﻿using Core.Cqrs.Abstractions.Commands;
using Notifications.Ui.DomainModels;

namespace Notifications.Ui.DomainServices.Notifications.Commands.Send
{
	class SendNotification:ICommand
	{
		public NotificationInfo Notification { get; set; }
	}
}