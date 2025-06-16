using MediatR;

namespace Moongazing.Incepta.Application.Features.IPBlocks.Commands.Delete;

public record DeleteIPBlockCommand(Guid Id) : IRequest<bool>;