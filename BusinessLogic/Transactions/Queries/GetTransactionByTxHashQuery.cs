using baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;
using MediatR;

namespace baseledger_replicator.BusinessLogic.Transactions.Queries;

public class GetTransactionByTxHashQuery : IRequest<bool>
{
    public string TxHash { get; set; }
}

public class GetTransactionByTxHashQueryHandler : IRequestHandler<GetTransactionByTxHashQuery, bool>
{
    private readonly ITransactionAgent _transactionAgent;

    public GetTransactionByTxHashQueryHandler(ITransactionAgent transactionAgent)
    {
        _transactionAgent = transactionAgent;
    }

    public async Task<bool> Handle(GetTransactionByTxHashQuery request, CancellationToken cancellationToken)
    {
        var result = await _transactionAgent.QueryTxByHash(request.TxHash);

        return result;
    }
}