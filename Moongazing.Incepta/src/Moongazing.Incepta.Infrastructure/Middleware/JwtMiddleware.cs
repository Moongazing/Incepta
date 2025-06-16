using Microsoft.AspNetCore.Http;
using Moongazing.Incepta.Infrastructure.Jwt;

namespace Moongazing.Incepta.Infrastructure.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IJwtTokenValidator _tokenValidator;

    public JwtMiddleware(RequestDelegate next, IJwtTokenValidator tokenValidator)
    {
        _next = next;
        _tokenValidator = tokenValidator;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader.Substring("Bearer ".Length);
            var principal = _tokenValidator.ValidateToken(token);

            if (principal != null)
            {
                context.User = principal;
            }
        }

        await _next(context);
    }
}
