using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionBodyDto
{
    [JsonProperty("body")]
    public string Body { get; set; }
}