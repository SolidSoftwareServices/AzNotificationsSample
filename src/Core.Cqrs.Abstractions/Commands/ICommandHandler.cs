using System.Threading.Tasks;

namespace Core.Cqrs.Abstractions.Commands
{
	public interface ICommandHandler<in TCommand> where TCommand : CqrsCommand
	{
		Task ExecuteAsync(TCommand command);
	}

}