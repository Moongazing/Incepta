using MediatR;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Create;

public record CreatePolicyCommand(string Name, List<string> RequiredClaims) : IRequest<Guid>;
