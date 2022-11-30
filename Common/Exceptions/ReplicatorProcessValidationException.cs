using Microsoft.AspNetCore.Identity;

namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorProcessValidationException : Exception
{
    public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

    public ReplicatorProcessValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ReplicatorProcessValidationException(string message) : base(message)
    {
        this.Errors = new Dictionary<string, List<string>>
            {
                { string.Empty, new List<string> { message } }
            };
    }

    public ReplicatorProcessValidationException(IEnumerable<IdentityError> errors) : base()
    {
        // BET-1327
        this.Errors = new Dictionary<string, List<string>>
            {
                { "Password", errors.Select(p => p.Code).ToList() }
            };
    }

    public ReplicatorProcessValidationException() : base()
    {
    }
}
