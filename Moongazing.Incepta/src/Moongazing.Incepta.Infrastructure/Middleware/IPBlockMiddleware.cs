using Microsoft.AspNetCore.Http;
using Moongazing.Incepta.Infrastructure.IPBlocking;

namespace Moongazing.Incepta.Infrastructure.Middleware;

public class IPBlockMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IIPBlockService _ipBlockService; 
    public IPBlockMiddleware(RequestDelegate next, IIPBlockService ipBlockService)
    {
        _next = next;
        _ipBlockService = ipBlockService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();
        if (ip is not null && await _ipBlockService.IsBlockedAsync(ip))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("IP is blocked.");
            return;
        }

        await _next(context);
    }
}
