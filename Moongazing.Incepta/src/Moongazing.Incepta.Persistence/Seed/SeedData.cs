using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Persistence.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(GatewayDbContext dbContext)
    {
        if (dbContext.Tenants.Any()) return;

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = "acme",
            Hostname = "acme.incepta.io",
            Routes = new List<ApiRoute>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Path = "/invoice/*",
                TargetUrl = "http://localhost:7001",
                RateLimit = 100,
                RateLimitWindowSeconds = 60,
                RequiredPolicies = new List<string> { "InvoicePolicy" },
                Enabled = true
            }
        }
        };

        var policy = new PolicyDefinition
        {
            Id = Guid.NewGuid(),
            Name = "InvoicePolicy",
            RequiredClaims = new List<string> { "role:admin", "permission:invoice:add" }
        };

        await dbContext.Tenants.AddAsync(tenant);
        await dbContext.Policies.AddAsync(policy);
        await dbContext.SaveChangesAsync();
    }
}