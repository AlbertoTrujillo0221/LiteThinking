using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using MediatR;
namespace Application.UseCases.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : IRequest<Result<GetTransactionsQueryDto>>
{
    public class GetTransactionsQueryHandler(
        IRepository<Transaction> transaccionRepository) : UseCaseHandler, IRequestHandler<GetTransactionsQuery, Result<GetTransactionsQueryDto>>
    {
        public async Task<Result<GetTransactionsQueryDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await transaccionRepository.GetAllAsync();

            var transactionsDto = transactions
                    .Select(x => new GetTransactionsQueryValueDto()
                    {
                        Id = x.Id,
                        Value = x.Value,
                        Status = x.Status,
                        Date = x.Date
                    });

            var resultData = new GetTransactionsQueryDto()
            {
                Transactions = transactionsDto
            };

            return this.Succeded(resultData);
        }
    }
}
