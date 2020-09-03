using System.Threading.Tasks;

namespace Core.Cqrs.Abstractions.Commands
{
	public interface ICommandDispatcher
	{
		Task ExecuteAsync<TCommand>(TCommand command) where TCommand : CqrsCommand;
	}

	public interface IInProcCommandDispatcher: ICommandDispatcher
	{
		Task ExecuteAsync(object command);
	}
}