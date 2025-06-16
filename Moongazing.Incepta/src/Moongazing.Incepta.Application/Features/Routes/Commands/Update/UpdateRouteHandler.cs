using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Update;

public class UpdateRouteHandler : IRequestHandler<UpdateRouteCommand, bool>
{
    private readonly GatewayDbContext _db;
    public UpdateRouteHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Routes.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        entity.Path = request.Path;
        entity.TargetUrl = request.TargetUrl;
        entity.Enabled = request.Enabled;

        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}