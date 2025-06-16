using MediatR;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Delete;

public record DeleteTenantCommand(Guid Id) : IRequest<bool>;
