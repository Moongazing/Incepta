using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Delete;

public class DeleteTenantHandler : IRequestHandler<DeleteTenantCommand, bool>
{
    private readonly GatewayDbContext _db;
    public DeleteTenantHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Tenants.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        _db.Tenants.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}