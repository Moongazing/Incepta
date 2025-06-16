using MediatR;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Application.Features.Policy.Commands.Delete;

public class DeletePolicyHandler : IRequestHandler<DeletePolicyCommand, bool>
{
    private readonly GatewayDbContext _db;

    public DeletePolicyHandler(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.Policies.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        _db.Policies.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}