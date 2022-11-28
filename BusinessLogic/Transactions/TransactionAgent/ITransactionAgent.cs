namespace baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;

public interface ITransactionAgent
{
    Task<bool> QueryTxByHash (string txHash);

    Task<string> CreateTransaction (Guid transactionId, string payload);
}