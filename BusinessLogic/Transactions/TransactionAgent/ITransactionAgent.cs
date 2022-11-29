using baseledger_replicator.DTOs.Transactions;

namespace baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;

public interface ITransactionAgent
{
    Task<TransactionResponseDto> QueryTxByHash (string txHash);

    Task<string> CreateTransaction (Guid transactionId, string payload);
}