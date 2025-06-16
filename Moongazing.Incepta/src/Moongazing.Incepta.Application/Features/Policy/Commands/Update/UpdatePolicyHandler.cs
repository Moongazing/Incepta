using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Update;

public class UpdatePolicyHandler : IRequestHandler<UpdatePolicyCommand, bool>
{
    private readonly GatewayDbContext _db;

    public UpdatePolicyHandler(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Policies.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        entity.Name = request.Name;
        entity.RequiredClaims = request.RequiredClaims;

        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}