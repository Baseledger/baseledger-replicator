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
    public async Task<ActionResult> GetTxByHash([FromRoute] string txHash)
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
    public async Task<ActionResult> CreateTransactionInReplicatorNode([FromBody] CreateTransactionDto body)
    {
        var txHash = await Mediator.Send(new CreateTransactionInReplicatorNodeCommand
        {
            TransactionId = body.TransactionId,
            Payload = body.Payload
        });

        // TODO: How and in what detail do we want to process errors and return them to the API caller?
        return txHash != null ? Ok(txHash) : Ok("Error when creating transaction in replicator node.");
    }
}
