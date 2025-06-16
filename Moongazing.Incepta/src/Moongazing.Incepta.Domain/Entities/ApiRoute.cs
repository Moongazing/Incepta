namespace Moongazing.Incepta.Domain.Entities;

public class ApiRoute
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Path { get; set; } = null!;
    public string TargetUrl { get; set; } = null!;
    public List<string> RequiredPolicies { get; set; } = new();
    public int RateLimit { get; set; }
    public int RateLimitWindowSeconds { get; set; }
    public bool Enabled { get; set; } = true;
    public Guid TenantId { get; set; }
}
