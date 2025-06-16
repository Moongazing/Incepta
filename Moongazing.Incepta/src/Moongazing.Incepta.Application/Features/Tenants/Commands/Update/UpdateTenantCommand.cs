using MediatR;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Update;

public record UpdateTenantCommand(Guid Id, string Name, string Hostname) : IRequest<bool>;
