using Application.Common.Interfaces;
using Application.UseCases.Common.Handlers;
using Application.UseCases.Common.Results;
using Domain.Entities;
using MediatR;
namespace Application.UseCases.Transactions.Queries.GetTransactions;

public class GetTransactionsQuery : IRequest<Result<GetTransactionsQueryDto>>
{
    public class GetTransactionsQueryHandler(
        IRepository<Transaction> transaccionRepository,
        IExternalService<Domain.Entities.Transactions> externalService,
        IExternalService<FilesName> externalServiceString) : UseCaseHandler, IRequestHandler<GetTransactionsQuery, Result<GetTransactionsQueryDto>>
    {
        public async Task<Result<GetTransactionsQueryDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var response = await transaccionRepository.GetAllAsync();

            var transactionsDto = response
                    .Select(x => new GetTransactionsQueryValueDto()
                    {
                        Id = x.Id,
                        Value = x.Value,
                        Status = x.Status,
                        Date = x.Date
                    });

            var names = await externalServiceString.GetList();

            var transactions = new Domain.Entities.Transactions()
            {
                transactions = new List<FileEntity>()
                {
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        FileName = "Microservice Test 1",
                        TransactionName = "Microservice Transaction 1"
                    },
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        FileName = "Microservice Test 11",
                        TransactionName = "Microservice Transaction 11"
                    },
                }
            };

            var isSucces = await externalService.Create(transactions);

            var resultData = new GetTransactionsQueryDto()
            {
                Transactions = transactionsDto
            };

            return this.Succeded(resultData);
        }
    }
}
