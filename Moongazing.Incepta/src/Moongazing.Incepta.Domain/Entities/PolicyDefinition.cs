namespace Moongazing.Incepta.Domain.Entities;

public class PolicyDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public List<string> RequiredClaims { get; set; } = new();
}