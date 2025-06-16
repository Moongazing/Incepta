using MediatR;
using Moongazing.Incepta.Application.Features.Routes.Dtos;

namespace Moongazing.Incepta.Application.Features.Routes.Queries.GetById;

public record GetRouteByIdQuery(Guid Id) : IRequest<RouteDto?>;
