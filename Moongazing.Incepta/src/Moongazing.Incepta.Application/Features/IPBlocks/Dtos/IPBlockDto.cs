namespace Moongazing.Incepta.Application.Features.IPBlocks.Dtos;

public class IPBlockDto
{
    public Guid Id { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}