using baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;
using baseledger_replicator.DTOs.Transactions;
using MediatR;

namespace baseledger_replicator.BusinessLogic.Transactions.Queries;

public class GetTransactionByTxHashQuery : IRequest<TransactionResponseDto>
{
    public string TxHash { get; set; }
}

public class GetTransactionByTxHashQueryHandler : IRequestHandler<GetTransactionByTxHashQuery, TransactionResponseDto>
{
    private readonly ITransactionAgent _transactionAgent;

    public GetTransactionByTxHashQueryHandler(ITransactionAgent transactionAgent)
    {
        _transactionAgent = transactionAgent;
    }

    public async Task<TransactionResponseDto> Handle(GetTransactionByTxHashQuery request, CancellationToken cancellationToken)
    {
        return await _transactionAgent.QueryTxByHash(request.TxHash);
    }
}