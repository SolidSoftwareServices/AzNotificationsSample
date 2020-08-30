using System.Threading.Tasks;

namespace Core.Cqrs.Abstractions.Queries
{
	public interface IQueryResolver
	{
		Task<TResult> ExecuteAsync<TQuery,TResult>(TQuery query) where TQuery : IQuery;
	}
}