using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Moongazing.Incepta.Infrastructure.Jwt;

public class JwtTokenValidator : IJwtTokenValidator
{
    private readonly TokenValidationParameters _validationParameters;

    public JwtTokenValidator(IConfiguration config)
    {
        var key = config["Jwt:Key"] ?? throw new Exception("Jwt:Key missing");
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];

        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        try
        {
            return handler.ValidateToken(token, _validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }
}