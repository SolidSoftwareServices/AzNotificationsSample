using System.Threading.Tasks;

namespace Core.Cqrs.Abstractions.Commands
{
	public interface ICommandDispatcher
	{
		Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
	}
}