using MediatR;
using Moongazing.Incepta.Application.Features.Tenants.Dtos;

namespace Moongazing.Incepta.Application.Features.Tenants.Queries.GetAll;

public record GetAllTenantsQuery() : IRequest<List<TenantDto>>;
