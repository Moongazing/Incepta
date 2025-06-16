using Microsoft.AspNetCore.Http;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Infrastructure.Redis;

namespace Moongazing.Incepta.Infrastructure.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimiterService _rateLimiter;

    public RateLimitMiddleware(RequestDelegate next, IRateLimiterService rateLimiter)
    {
        _next = next;
        _rateLimiter = rateLimiter;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenant = context.Items["Tenant"] as Tenant;
        var route = context.Items["MatchedRoute"] as ApiRoute;

        if (tenant == null || route == null)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Tenant or route not resolved");
            return;
        }

        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var key = $"{tenant.Id}:{route.Path}:{ip}";

        var allowed = await _rateLimiter.IsRequestAllowedAsync(key, route.RateLimit, route.RateLimitWindowSeconds);

        if (!allowed)
        {
            context.Response.StatusCode = 429;
            await context.Response.WriteAsync("Rate limit exceeded");
            return;
        }

        await _next(context);
    }
}