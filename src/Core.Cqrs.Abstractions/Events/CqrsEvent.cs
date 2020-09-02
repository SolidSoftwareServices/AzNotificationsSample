using System;

namespace Core.Cqrs.Abstractions.Events
{
	public abstract class CqrsEvent
	{
		public abstract string Name { get; }
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
	}
}