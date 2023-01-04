namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorNodeException : Exception
{
    public ReplicatorNodeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ReplicatorNodeException(string message) : base(message)
    {
    }

    public ReplicatorNodeException() : base()
    {
    }
}
