using MediatR;

namespace Moongazing.Incepta.Application.Features.IPBlocks.Commands.Create;

public record CreateIPBlockCommand(string IPAddress, string? Reason, DateTime? ExpiresAt) : IRequest<Guid>;