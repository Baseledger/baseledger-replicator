namespace baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;

public class TransactionAgent : ITransactionAgent
{
    private readonly string url = "http://127.0.0.1:1317/cosmos/tx/v1beta1/txs/";

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
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while retrieving transaction: {ex.Message} for txHash: {txHash}");
        }

        return true;
    }
}