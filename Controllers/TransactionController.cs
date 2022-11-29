using baseledger_replicator.BusinessLogic.Transactions.Commands;
using baseledger_replicator.BusinessLogic.Transactions.Queries;
using baseledger_replicator.DTOs.Transactions;
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
    [Produces("application/json")]
    public async Task<IActionResult> GetTxByHash([FromRoute] string txHash)
    {
        var result = await Mediator.Send(new GetTransactionByTxHashQuery { TxHash = txHash });
        return Ok(result);
    }

    /// <summary>
    /// Creates a transaction in the replicator node
    /// </summary>
    /// <param name="body">CreateTransactionDto containing transaction ID and payload</param>
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateTransactionInReplicatorNode([FromBody] CreateTransactionDto body)
    {
        var txHash = await Mediator.Send(new CreateTransactionInReplicatorNodeCommand
        {
            TransactionId = body.TransactionId,
            Payload = body.Payload
        });

        // return Ok txhash
        // custom exception handler
        // not found
        // bad request
        return txHash != null ? Ok(txHash) : Ok("Error when creating transaction in replicator node.");
    }
}
