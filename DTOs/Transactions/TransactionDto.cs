using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionDto
{
    [JsonProperty("body")]
    public TransactionBodyDto Body { get; set; }
}