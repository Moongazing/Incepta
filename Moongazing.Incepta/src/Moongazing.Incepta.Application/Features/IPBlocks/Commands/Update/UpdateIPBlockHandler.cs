using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Update;
using Moongazing.Incepta.Persistence.Context;

public class UpdateIPBlockHandler : IRequestHandler<UpdateIPBlockCommand, bool>
{
    private readonly GatewayDbContext _db;
    public UpdateIPBlockHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(UpdateIPBlockCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.IPBlocks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        entity.IPAddress = request.IPAddress;
        entity.Reason = request.Reason;
        entity.ExpiresAt = request.ExpiresAt;

        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
