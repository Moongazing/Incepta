namespace Moongazing.Incepta.Domain.Entities;

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Hostname { get; set; } = null!;
    public List<ApiRoute> Routes { get; set; } = new();
}