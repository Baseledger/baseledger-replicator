
namespace baseledger_replicator.Services;

// TODO: We have this solely for the purpose of getting traceId; if we don't need it, we can remove it
public interface ITraceIdentifierService
{
    /// <summary>
    /// Current trace id used for logging and error tracking
    /// </summary>
    string TraceId { get; }
}
