using System;

namespace Notifications.Ui.DomainModels
{
	public class NotificationInfo
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime Date { get; set; }=DateTime.UtcNow;

		public string Text{ get; set; }

		public NotificationStatus Status { get; set; }

		public UserInfo From { get; set; }
		public UserInfo To { get; set; }

		public override string ToString()
		{
			return $"{nameof(Date)}: {Date}, {nameof(Text)}: {Text}, {nameof(Status)}: {Status}, {nameof(From)}: {From}, {nameof(To)}: {To}";
		}

		public enum NotificationStatus
		{
			New=1,
			Sent,
			ReadCompleted,
			Errored

		}
	}

	
}
