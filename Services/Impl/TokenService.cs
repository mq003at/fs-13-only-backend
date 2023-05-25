namespace store.Services;

using store.DTOs;
using store.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Text.Json;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration config, UserManager<User> userManager)
    {
        _config = config;
        _userManager = userManager;
    }

    public async Task<CredentialDTO> GenerateTokenAsync(User user, string? purpose)
    {
        // Payload
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            new Claim("role", user.Role.ToString()),
        };

        // Secret
        string secret;
        double expTime;

        if (purpose != null && purpose.Equals("rememberMe"))
        {
            secret = _config["Jwt:RememberSecret"] ?? "PlaceholderSecret";
            expTime =
                double.TryParse(_config["Jwt:RememberExpiration"], out double doubleResult)
                && doubleResult > 0
                    ? doubleResult
                    : 5;
        }
        else
        {
            secret = _config["Jwt:Secret"] ?? "PlaceholderSecret";
            expTime =
                double.TryParse(_config["Jwt:Expiration"], out double doubleResult)
                && doubleResult > 0
                    ? doubleResult
                    : 5;
        }
        string issuer = _config["Jwt:Issuer"] ?? "PlaceholderCompany";

        var signingKey = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            SecurityAlgorithms.HmacSha256
        );

        // Expiration
        var expiration = DateTime.Now.AddHours(expTime);

        var token = new JwtSecurityToken(
            issuer,
            claims: claims,
            expires: expiration,
            signingCredentials: signingKey
        );
        var tokenWriter = new JwtSecurityTokenHandler();

        var cred = new CredentialDTO()
        {
            Token = tokenWriter.WriteToken(token),
            Expiration = expiration,
        };

        if (purpose != null)
        {
            cred.Purpose = purpose;
        }
        await Task.CompletedTask;
        return cred;
    }
}
