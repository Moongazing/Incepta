using MediatR;
using Moongazing.Incepta.Application.Features.Tenants.Dtos;

namespace Moongazing.Incepta.Application.Features.Tenants.Queries.GetById;

public record GetTenantByIdQuery(Guid Id) : IRequest<TenantDto?>;
