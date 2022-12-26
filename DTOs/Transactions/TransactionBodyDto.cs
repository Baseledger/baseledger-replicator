using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionBodyDto
{
    [JsonProperty("messages")]
    public List<TransactionMessageDto> Messages { get; set; }
}