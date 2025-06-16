using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Infrastructure.IPBlocking;
using Moongazing.Incepta.Infrastructure.Jwt;
using Moongazing.Incepta.Infrastructure.Middleware;
using Moongazing.Incepta.Infrastructure.Redis;
using Moongazing.Incepta.Persistence.Context;
using Moongazing.Incepta.Persistence.Seed;
using StackExchange.Redis;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<GatewayDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(
    builder.Configuration.GetConnectionString("Redis")!));


builder.Services.AddScoped<IIPBlockService, EfIpBlockService>();
builder.Services.AddScoped<IRateLimiterService, RedisRateLimiter>();
builder.Services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();
builder.Services.AddCors();
builder.Services.AddRouting();
builder.Services.AddReverseProxy()
    .LoadFromMemory(Array.Empty<RouteConfig>(), Array.Empty<ClusterConfig>()); 

var app = builder.Build();



app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



app.UseMiddleware<IPBlockMiddleware>();           
app.UseMiddleware<TenantResolverMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<PolicyMiddleware>();

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.Use((context, next) =>
    {
        var route = context.Items["MatchedRoute"] as ApiRoute;
        context.Request.Headers.Remove("X-Forwarded-Host");
        context.Request.Headers["X-Forwarded-For"] = context.Connection.RemoteIpAddress?.ToString();

        context.Request.Path = PathString.Empty;
        context.Request.PathBase = "";

        context.Request.Headers["X-Tenant"] = (context.Items["Tenant"] as Tenant)?.Name ?? "unknown";

        return next();
    });
});



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GatewayDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.InitializeAsync(db);
}

await app.RunAsync();
