namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorNotFoundException : Exception
{
    public ReplicatorNotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }

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
