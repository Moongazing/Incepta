using MediatR;
using Moongazing.Incepta.Application.Features.Routes.Dtos;

namespace Moongazing.Incepta.Application.Features.Routes.Queries.GetAll;

public record GetAllRoutesQuery(Guid? TenantId = null) : IRequest<List<RouteDto>>;
