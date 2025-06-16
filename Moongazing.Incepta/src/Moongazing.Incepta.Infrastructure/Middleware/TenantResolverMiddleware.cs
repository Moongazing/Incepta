using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Persistence.Context;
using System.Net.Http;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;
    private readonly GatewayDbContext _db;

    public TenantResolverMiddleware(RequestDelegate next, GatewayDbContext db)
    {
        _next = next;
        _db = db;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var host = context.Request.Host.Host.ToLower();
        var tenant = await _db.Tenants.Include(t => t.Routes)
                                      .FirstOrDefaultAsync(t => t.Hostname.ToLower() == host);

        if (tenant == null)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Tenant not found");
            return;
        }

        var path = context.Request.Path.Value?.ToLower() ?? "";
        var matchedRoute = tenant.Routes.FirstOrDefault(r => path.StartsWith(r.Path.TrimEnd('*').ToLower()));

        if (matchedRoute == null || !matchedRoute.Enabled)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Route not found");
            return;
        }

        context.Items["Tenant"] = tenant;
        context.Items["MatchedRoute"] = matchedRoute;

        await _next(context);
    }
}