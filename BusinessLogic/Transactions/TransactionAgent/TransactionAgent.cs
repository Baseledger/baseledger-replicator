using Newtonsoft.Json;

namespace baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;

public class TransactionAgent : ITransactionAgent
{
    // base: http://0.0.0.0:1317
    private readonly string url = "http://0.0.0.0:1317/cosmos/tx/v1beta1/txs/";

    private readonly ILogger<TransactionAgent> _logger;

    public TransactionAgent(ILogger<TransactionAgent> logger)
    {
        _logger = logger;
    }

    public async Task<bool> QueryTxByHash(string txHash)
    {
        var httpClient = new HttpClient();

        var uri = new Uri(this.url + txHash);

        var response = await httpClient.GetAsync(uri);

        try
        {
            response.EnsureSuccessStatusCode();
            var responseString = response.Content.ReadAsStringAsync().Result;
            // TODO: add response body deserialization
            // string something = JsonConvert.DeserializeObject<dynamic>(responseStr).property;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while retrieving transaction: {ex.Message} for txHash: {txHash}");
        }

        return true;
    }

    public async Task<string> CreateTransaction(Guid transactionId, string payload)
    {
        var httpClient = new HttpClient();
        // ovo ide na base
        var uri = new Uri(this.url + "signAndBroadcast");
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
            _logger.LogError($"Error in creating transaction for transactionId: {transactionId} with message: {ex.Message} \nInner exception: {ex.InnerException?.Message}");
            // TODO: throw custom exception
            return null;
        }

        return response.Content.ReadAsStringAsync().Result;
    }
}