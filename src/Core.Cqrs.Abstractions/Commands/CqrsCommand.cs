using System;

namespace Core.Cqrs.Abstractions.Commands
{
	public abstract class CqrsCommand
	{
		public Guid Id { get; private set; }=Guid.NewGuid();
	}
}