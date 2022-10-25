using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace baseledger_replicator.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly string _jwtSecretKey;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSecretKey = _configuration["JWT:Secret"];
    }

    public string GetJwtTokenString(IdentityUser user)
    {
        var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSecretKey));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email)
            };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}