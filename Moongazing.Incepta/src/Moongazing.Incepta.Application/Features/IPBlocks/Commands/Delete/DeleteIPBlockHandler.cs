using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Delete;
using Moongazing.Incepta.Persistence.Context;

public class DeleteIPBlockHandler : IRequestHandler<DeleteIPBlockCommand, bool>
{
    private readonly GatewayDbContext _db;
    public DeleteIPBlockHandler(GatewayDbContext db) => _db = db;

    public async Task<bool> Handle(DeleteIPBlockCommand request, CancellationToken cancellationToken)
    {
        var entity = await _db.IPBlocks.FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null) return false;

        _db.IPBlocks.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
