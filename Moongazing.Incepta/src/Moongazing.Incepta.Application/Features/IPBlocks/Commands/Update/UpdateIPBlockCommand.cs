using MediatR;

namespace Moongazing.Incepta.Application.Features.IPBlocks.Commands.Update;

public record UpdateIPBlockCommand(Guid Id, string IPAddress, string? Reason, DateTime? ExpiresAt) : IRequest<bool>;