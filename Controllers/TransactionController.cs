using baseledger_replicator.BusinessLogic.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baseledger_replicator.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TransactionController : BaseController
{
    public TransactionController()
    {

    }

    /// <summary>
    /// Retrieves transaction by txHash
    /// </summary>
    /// <param name="txHash">Transaction hash</param>
    [HttpGet("transaction/{txhash}")]
    public async Task<ActionResult<bool>> GetTxByHash([FromRoute] string txHash)
    {
        var result = await Mediator.Send(new GetTransactionByTxHashQuery { TxHash = txHash });
        return Ok(result);
    }
}
