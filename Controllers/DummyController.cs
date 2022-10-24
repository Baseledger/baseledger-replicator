using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baseledger_replicator.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DummyController : BaseController
{
    public DummyController()
    {

    }

    /// <summary>
    /// Gets random integer
    /// </summary>
    /// <returns>Random integer</returns>
    [HttpGet(Name = "GetRandomInt")]
    public int Get()
    {
        return Random.Shared.Next();
    }
}
