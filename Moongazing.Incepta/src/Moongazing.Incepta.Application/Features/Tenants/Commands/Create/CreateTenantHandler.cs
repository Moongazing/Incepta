using MediatR;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Tenants.Commands.Create;

public class CreateTenantHandler : IRequestHandler<CreateTenantCommand, Guid>
{
    private readonly GatewayDbContext _db;
    public CreateTenantHandler(GatewayDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var entity = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Hostname = request.Hostname
        };

        _db.Tenants.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
