using System;
using System.Threading.Tasks;
using Core.Cqrs.Abstractions.Queries;
using Core.System;

namespace Core.Cqrs.Queries
{
	class InProcQueryResolver:IQueryResolver
	{

		private readonly IServiceProvider _serviceProvider;

		public InProcQueryResolver(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task<TResult> ExecuteAsync<TQuery,TResult>(TQuery query) where TQuery : IQuery
		{
			var queryHandler = _serviceProvider
				.Resolve<IQueryHandler<TQuery, TResult>>();

			return await queryHandler.ExecuteAsync(query);
		}
	}
}
