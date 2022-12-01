using AutoMapper;
using baseledger_replicator.Common.Exceptions;
using baseledger_replicator.DTOs.Auth;
using baseledger_replicator.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace baseledger_replicator.BusinessLogic.Auth.Commands;

public class LoginCommand : IRequest<LoggedUserDto>
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedUserDto>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public LoginCommandHandler(UserManager<IdentityUser> userManager, ILogger<LoginCommand> logger, IJwtService jwtService, IMapper mapper)
    {
        _userManager = userManager;
        _logger = logger;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<LoggedUserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser == null || !existingUser.EmailConfirmed)
        {
            _logger.LogError($"User with email: {request.Email} not found!");
            throw new ReplicatorNotFoundException($"User with email: {request.Email} not found!");
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);

        if (isPasswordCorrect)
        {
            var jwtToken = _jwtService.GetJwtTokenString(existingUser);
            var loggedUser = _mapper.Map<IdentityUser, LoggedUserDto>(existingUser);
            loggedUser.Token = jwtToken;

            return loggedUser;
        }
        else
        {
            _logger.LogError($"Invalid password for user: {existingUser.Email}");
            throw new ReplicatorValidationException($"Invalid password for user: {existingUser.Email}");
        }
    }
}