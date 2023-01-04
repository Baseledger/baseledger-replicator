namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorValidationException : Exception
{
    public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

    public ReplicatorValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ReplicatorValidationException(string message) : base(message)
    {
        this.Errors = new Dictionary<string, List<string>>
            {
                { string.Empty, new List<string> { message } }
            };
    }

    public ReplicatorValidationException() : base()
    {
    }
}
