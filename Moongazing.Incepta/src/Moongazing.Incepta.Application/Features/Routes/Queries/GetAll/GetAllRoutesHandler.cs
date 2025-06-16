using MediatR;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Application.Features.Routes.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Routes.Queries.GetAll;

public class GetAllRoutesHandler : IRequestHandler<GetAllRoutesQuery, List<RouteDto>>
{
    private readonly GatewayDbContext _db;
    public GetAllRoutesHandler(GatewayDbContext db) => _db = db;

    public async Task<List<RouteDto>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
    {
        var query = _db.Routes.AsQueryable();
        if (request.TenantId.HasValue)
            query = query.Where(r => r.TenantId == request.TenantId);

        return await query
            .Select(r => new RouteDto
            {
                Id = r.Id,
                Path = r.Path,
                TargetUrl = r.TargetUrl,
                Enabled = r.Enabled,
                TenantId = r.TenantId
            })
            .ToListAsync(cancellationToken);
    }
}
