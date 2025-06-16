using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Commands.Create;
using Moongazing.Incepta.Domain.Entities;
using Moongazing.Incepta.Persistence.Context;

public class CreateIPBlockHandler : IRequestHandler<CreateIPBlockCommand, Guid>
{
    private readonly GatewayDbContext _db;
    public CreateIPBlockHandler(GatewayDbContext db) => _db = db;

    public async Task<Guid> Handle(CreateIPBlockCommand request, CancellationToken cancellationToken)
    {
        var entity = new IPBlock
        {
            Id = Guid.NewGuid(),
            IPAddress = request.IPAddress,
            Reason = request.Reason,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = request.ExpiresAt
        };

        _db.IPBlocks.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
