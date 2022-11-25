using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using baseledger_replicator.BusinessLogic.Auth.Commands;
using baseledger_replicator.DTOs.Auth;
using baseledger_replicator.Models;
using Microsoft.AspNetCore.Authorization;

namespace baseledger_replicator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly BaseledgerReplicatorContext _baseledgerReplicatorContext;
    private readonly ILogger _logger;

    public AccountController(UserManager<IdentityUser> userManager, BaseledgerReplicatorContext baseledgerReplicatorContext, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _baseledgerReplicatorContext = baseledgerReplicatorContext;
        _logger = logger;
    }
    // TODO: Remove this endpoint after testing

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="registerUserDto"> Contains the register user dto</param>
    /// <response code="200">Successful registration</response>
    /// <response code="400">If an error occured during registration</response>
    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string[]))]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerUserDto)
    {
        var newUser = new IdentityUser()
        {
            Email = registerUserDto.Email
        };

        if (string.IsNullOrEmpty(newUser.UserName))
        {
            newUser.UserName = newUser.Email;
        }

        newUser.EmailConfirmed = true;

        var creationResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
        return Ok();
    }

    /// <summary>
    /// Login user, returns information about the logged user with the token
    /// </summary>
    /// <param name="loginDto">login Dto</param>
    /// <response code="200">Successful login, JWT token returned</response>
    /// <response code="400">If an error occured during login</response>
    /// <response code="404">User not found</response>
    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoggedUserDto))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string[]))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var loggedUser = await Mediator.Send(new LoginCommand { Email = loginDto.Email, Password = loginDto.Password });
        return Ok(loggedUser);
    }
}