using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class TransactionResponseDto
{
    [JsonProperty("height")]
    public string TxHeight { get; set; }

    [JsonProperty("txhash")]
    public string TxHash { get; set; }

    [JsonProperty("data")]
    public string Payload { get; set; }
}