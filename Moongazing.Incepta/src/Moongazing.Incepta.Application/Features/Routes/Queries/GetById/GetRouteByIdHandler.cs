using MediatR;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Application.Features.Routes.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Routes.Queries.GetById;

public class GetRouteByIdHandler : IRequestHandler<GetRouteByIdQuery, RouteDto?>
{
    private readonly GatewayDbContext _db;
    public GetRouteByIdHandler(GatewayDbContext db) => _db = db;

    public async Task<RouteDto?> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Routes
            .Where(r => r.Id == request.Id)
            .Select(r => new RouteDto
            {
                Id = r.Id,
                Path = r.Path,
                TargetUrl = r.TargetUrl,
                Enabled = r.Enabled,
                TenantId = r.TenantId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}