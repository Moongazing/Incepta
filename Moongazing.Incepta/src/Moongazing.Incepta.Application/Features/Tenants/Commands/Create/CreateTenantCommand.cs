using MediatR;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Create;

public record CreateTenantCommand(string Name, string Hostname) : IRequest<Guid>;
