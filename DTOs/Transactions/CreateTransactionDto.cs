namespace baseledger_replicator.DTOs.Transactions;

public class CreateTransactionDto
{
    public Guid TransactionId { get; set; }

    public string Payload { get; set; }
}