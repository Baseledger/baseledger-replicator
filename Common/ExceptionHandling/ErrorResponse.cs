using System.Collections.Generic;

namespace baseledger_replicator.Common.ExceptionHandling;

public class ErrorResponse
{
    public string TraceId { get; set; }

    public string Message { get; set; }

    public Dictionary<string, List<string>> Errors { get; set; }
}
