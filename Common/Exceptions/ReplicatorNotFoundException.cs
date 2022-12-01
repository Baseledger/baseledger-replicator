namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorNotFoundException : Exception
{
    public ReplicatorNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ReplicatorNotFoundException(string message) : base(message)
    {
    }

    public ReplicatorNotFoundException() : base()
    {
    }
}
