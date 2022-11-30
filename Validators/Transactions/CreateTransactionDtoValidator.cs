using System.ComponentModel.DataAnnotations;
using baseledger_replicator.DTOs.Transactions;
using FluentValidation;

namespace baseledger_replicator.Validators.Transactions;

public class CreateTransactionDtoValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionDtoValidator()
    {
        RuleFor(transaction => transaction.TransactionId).NotNull().Must(x => x != Guid.Empty).WithMessage("TransactionId must not be null or empty Guid (zero values).");
        RuleFor(transaction => transaction.Payload).NotNull().NotEmpty().WithMessage("Payload must not be null or empty.");
    }
}