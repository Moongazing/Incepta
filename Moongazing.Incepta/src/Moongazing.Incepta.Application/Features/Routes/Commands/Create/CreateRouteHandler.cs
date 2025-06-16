using MediatR;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Routes.Commands.Create;

public class CreateRouteHandler : IRequestHandler<CreateRouteCommand, Guid>
{
    private readonly GatewayDbContext _db;
    public CreateRouteHandler(GatewayDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApiRoute
        {
            Id = Guid.NewGuid(),
            TenantId = request.TenantId,
            Path = request.Path,
            TargetUrl = request.TargetUrl,
            Enabled = request.Enabled
        };

        _db.Routes.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}