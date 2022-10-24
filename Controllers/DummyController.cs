using Microsoft.AspNetCore.Mvc;

namespace baseledger_replicator.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyController : ControllerBase
{
    public DummyController()
    {
        
    }

    [HttpGet(Name = "GetRandomInt")]
    public int Get()
    {
        return Random.Shared.Next();
    }
}
