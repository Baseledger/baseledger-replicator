using AutoMapper;
using baseledger_replicator.BusinessLogic.Transactions.TransactionAgent;
using baseledger_replicator.DTOs.Auth;
using baseledger_replicator.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace baseledger_replicator.BusinessLogic.Transactions.Commands;

public class CreateTransactionInReplicatorNodeCommand : IRequest<string>
{
    public Guid TransactionId { get; set; }

    public string Payload { get; set; }
}

public class CreateTransactionInReplicatorNodeCommandHandler : IRequestHandler<CreateTransactionInReplicatorNodeCommand, string>
{
    private readonly ILogger _logger;
    private readonly ITransactionAgent _transactionAgent;

    public CreateTransactionInReplicatorNodeCommandHandler(ILogger<CreateTransactionInReplicatorNodeCommand> logger, ITransactionAgent transactionAgent)
    {
        _logger = logger;
        _transactionAgent = transactionAgent;
    }

    public async Task<string> Handle(CreateTransactionInReplicatorNodeCommand request, CancellationToken cancellationToken)
    {
        return await _transactionAgent.CreateTransaction(request.TransactionId, request.Payload);
    }
}