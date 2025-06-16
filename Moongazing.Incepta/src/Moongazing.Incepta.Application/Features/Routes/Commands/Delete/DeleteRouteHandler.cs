using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Delete;

public class DeleteRouteHandler : IRequestHandler<DeleteRouteCommand, bool>
{
    private readonly GatewayDbContext _db;
    public DeleteRouteHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Routes.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        _db.Routes.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
