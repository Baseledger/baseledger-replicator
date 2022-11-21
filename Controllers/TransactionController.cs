using baseledger_replicator.BusinessLogic.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baseledger_replicator.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TransactionController : BaseController
{
    public TransactionController()
    {

    }

    /// <summary>
    /// Retrieves transaction by txHash
    /// </summary>
    /// <param name="txHash">Transaction hash</param>
    [HttpGet("{txHash}")]
    public async Task<ActionResult> GetTxByHash([FromRoute] string txHash)
    {
        var result = await Mediator.Send(new GetTransactionByTxHashQuery { TxHash = txHash });
        return Ok(result);
    }
}
