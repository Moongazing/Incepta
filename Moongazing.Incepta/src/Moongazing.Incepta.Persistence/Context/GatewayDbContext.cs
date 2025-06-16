using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Domain.Entities;

namespace Moongazing.Incepta.Persistence.Context;

public class GatewayDbContext : DbContext
{
    public GatewayDbContext(DbContextOptions<GatewayDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<ApiRoute> Routes => Set<ApiRoute>();
    public DbSet<PolicyDefinition> Policies => Set<PolicyDefinition>();
    public DbSet<IPBlock> IPBlocks => Set<IPBlock>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>()
            .HasMany(t => t.Routes)
            .WithOne()
            .HasForeignKey(r => r.TenantId);

        modelBuilder.Entity<ApiRoute>()
            .Property(r => r.RequiredPolicies)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<PolicyDefinition>()
            .Property(p => p.RequiredClaims)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
    }
}