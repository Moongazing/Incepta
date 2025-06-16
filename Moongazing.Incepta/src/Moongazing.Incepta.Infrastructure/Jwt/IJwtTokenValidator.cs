using System.Security.Claims;

namespace Moongazing.Incepta.Infrastructure.Jwt;

public interface IJwtTokenValidator
{
    ClaimsPrincipal? ValidateToken(string token);
}