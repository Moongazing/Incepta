using MediatR;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Delete;

public record DeletePolicyCommand(Guid Id) : IRequest<bool>;