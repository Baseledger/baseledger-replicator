using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionFullResponseDto
{
    [JsonProperty("tx_response")]
    public TransactionResponseDto TransactionResponseDto { get; set; }
}