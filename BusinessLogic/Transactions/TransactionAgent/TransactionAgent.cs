using System.Net;
using baseledger_replicator.Common.Exceptions;
using baseledger_replicator.DTOs.Transactions;
using Newtonsoft.Json;

namespace baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;

public class TransactionAgent : ITransactionAgent
{
    private readonly string baseUrl = "http://replicator:1317/";

    private readonly ILogger<TransactionAgent> _logger;

    public TransactionAgent(ILogger<TransactionAgent> logger)
    {
        _logger = logger;
    }

    public async Task<TransactionResponseDto> QueryTxByHash(string txHash)
    {
        var httpClient = new HttpClient();
        var uri = new Uri(this.baseUrl + "cosmos/tx/v1beta1/txs/" + txHash);

        HttpResponseMessage response;

        try
        {
            response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Transaction for txHash {txHash} not found. Error message: {ex.Message} \nInner exception: {ex.InnerException?.Message}");

            if (ex.StatusCode != null && ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ReplicatorNotFoundException($"Transaction for txHash {txHash} not found. Error message: {ex.Message}");
            }

            throw ex;
        }

        try
        {
            var responseString = response.Content.ReadAsStringAsync().Result;
            TransactionResponseDto txResponse = JsonConvert.DeserializeObject<TransactionFullResponseDto>(responseString).TransactionResponseDto;

            return txResponse;
        }
        catch (JsonSerializationException ex)
        {
            _logger.LogError($"Error trying to deserialize response when querying transaction by hash {txHash}: {ex.Message} \nInner exception: {ex.InnerException?.Message}");
            throw ex;
        }
    }

    public async Task<string> CreateTransaction(Guid transactionId, string payload)
    {
        var httpClient = new HttpClient();
        var uri = new Uri(this.baseUrl + "signAndBroadcast");
        var body = new
        {
            transaction_id = transactionId,
            payload = payload
        };

        HttpResponseMessage response;

        try
        {
            response = await httpClient.PostAsJsonAsync(uri, body);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error in creating transaction for transactionId {transactionId} with message: {ex.Message} \nInner exception: {ex.InnerException?.Message}");
            throw ex;
        }

        return response.Content.ReadAsStringAsync().Result;
    }
}