using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Update;

public class UpdateTenantHandler : IRequestHandler<UpdateTenantCommand, bool>
{
    private readonly GatewayDbContext _db;
    public UpdateTenantHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Tenants.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        entity.Name = request.Name;
        entity.Hostname = request.Hostname;

        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}