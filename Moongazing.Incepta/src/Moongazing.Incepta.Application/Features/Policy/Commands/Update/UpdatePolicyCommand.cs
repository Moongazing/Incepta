using MediatR;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Update;

public record UpdatePolicyCommand(Guid Id, string Name, List<string> RequiredClaims) : IRequest<bool>;
