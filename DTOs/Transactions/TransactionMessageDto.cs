using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionMessageDto
{
    [JsonProperty("baseledgerTransactionId")]
    public string BaseledgerTransactionId { get; set; }

    [JsonProperty("payload")]
    public string Payload { get; set; }

    [JsonProperty("opCode")]
    public int OpCode { get; set; }
}