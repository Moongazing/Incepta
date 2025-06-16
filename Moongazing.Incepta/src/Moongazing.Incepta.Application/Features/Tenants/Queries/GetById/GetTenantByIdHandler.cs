using MediatR;
using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Application.Features.Tenants.Dtos;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Tenants.Queries.GetById;

public class GetTenantByIdHandler : IRequestHandler<GetTenantByIdQuery, TenantDto?>
{
    private readonly GatewayDbContext _db;
    public GetTenantByIdHandler(GatewayDbContext db) => _db = db;

    public async Task<TenantDto?> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.Tenants
            .Where(t => t.Id == request.Id)
            .Select(t => new TenantDto
            {
                Id = t.Id,
                Name = t.Name,
                Hostname = t.Hostname
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}