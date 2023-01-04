using Newtonsoft.Json;

namespace baseledger_replicator.DTOs.Transactions;

public class ErrorResponseDto
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}