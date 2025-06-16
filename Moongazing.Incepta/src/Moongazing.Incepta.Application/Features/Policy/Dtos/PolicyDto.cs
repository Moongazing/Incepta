namespace Moongazing.Incepta.Application.Features.Policy.Dtos;

public class PolicyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> RequiredClaims { get; set; } = new();
}