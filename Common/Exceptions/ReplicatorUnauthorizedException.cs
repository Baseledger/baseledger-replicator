namespace baseledger_replicator.Common.Exceptions;

public class ReplicatorUnauthorizedException : Exception
{
    public ReplicatorUnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ReplicatorUnauthorizedException(string message) : base(message)
    {
    }

    public ReplicatorUnauthorizedException() : base()
    {
    }
}
