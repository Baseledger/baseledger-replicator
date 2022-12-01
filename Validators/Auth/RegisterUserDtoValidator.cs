using System.ComponentModel.DataAnnotations;
using baseledger_replicator.DTOs.Auth;
using baseledger_replicator.DTOs.Transactions;
using FluentValidation;

namespace baseledger_replicator.Validators.Auth;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(model => model.Email)
                .NotEmpty().WithMessage("You need to enter an e-mail address.")
                .EmailAddress().WithMessage("You need to enter a valid E-mail address.");

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage("You need to enter a password.");
    }
}