namespace Moongazing.Incepta.Application.Features.Routes.Dtos;

public class RouteDto
{
    public Guid Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public Guid TenantId { get; set; }
}