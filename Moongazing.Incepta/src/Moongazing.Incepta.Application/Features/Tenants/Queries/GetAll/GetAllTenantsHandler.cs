using MediatR;
using Moongazing.Incepta.Application.Features.Tenants.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Tenants.Queries.GetAll;

public class GetAllTenantsHandler : IRequestHandler<GetAllTenantsQuery, List<TenantDto>>
{
    private readonly GatewayDbContext _db;
    public GetAllTenantsHandler(GatewayDbContext db) => _db = db;

    public async Task<List<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Tenants
            .Select(t => new TenantDto
            {
                Id = t.Id,
                Name = t.Name,
                Hostname = t.Hostname
            })
            .ToListAsync(cancellationToken);
    }
}