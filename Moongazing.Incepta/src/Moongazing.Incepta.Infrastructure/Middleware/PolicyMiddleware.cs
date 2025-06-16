using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Infrastructure.Middleware;

public class PolicyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly GatewayDbContext _db;

    public PolicyMiddleware(RequestDelegate next, GatewayDbContext db)
    {
        _next = next;
        _db = db;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var route = context.Items["MatchedRoute"] as ApiRoute;
        if (route == null)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Route not resolved");
            return;
        }

        var requiredPolicies = route.RequiredPolicies;
        if (requiredPolicies == null || requiredPolicies.Count == 0)
        {
            await _next(context); 
            return;
        }

        var userClaims = context.User.Claims.Select(c => $"{c.Type}:{c.Value}").ToHashSet();

        foreach (var policyName in requiredPolicies)
        {
            var policy = await _db.Policies.FirstOrDefaultAsync(p => p.Name == policyName);
            if (policy == null)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Policy not found");
                return;
            }

            bool allClaimsMatch = policy.RequiredClaims.All(rc => userClaims.Contains(rc));

            if (!allClaimsMatch)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access denied: insufficient claims");
                return;
            }
        }

        await _next(context);
    }
}