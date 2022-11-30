using Microsoft.AspNetCore.Http;

namespace baseledger_replicator.Services;

// TODO: We have this solely for the purpose of getting traceId; if we don't need it, we can remove it
public class TraceIdentifierService : ITraceIdentifierService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public TraceIdentifierService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string TraceId
    {
        get
        {
            if (System.Diagnostics.Activity.Current != null)
            {
                return System.Diagnostics.Activity.Current.Id;
            }
            else if (httpContextAccessor.HttpContext != null)
            {
                return httpContextAccessor.HttpContext.TraceIdentifier;
            }
            else
            {
                // TODO: if we leave this, what should we say in this case?
                return "BackgroundService";
            }
        }
    }
}
