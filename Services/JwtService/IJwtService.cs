using Microsoft.AspNetCore.Identity;

namespace baseledger_replicator.Services;

public interface IJwtService
{
    /// <summary>
    /// Returns a JWT token string for a user
    /// </summary>
    /// <param name="user"> Identity user object</param>
    string GetJwtTokenString(IdentityUser user);
}