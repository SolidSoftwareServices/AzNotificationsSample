using System.Threading.Tasks;

namespace Core.Cqrs.Abstractions.Queries
{
	public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
	{
		Task<TResult> ExecuteAsync(TQuery query) ;
	}
}