using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Dtos;
using Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetById;
using Moongazing.Incepta.Persistence.Context;

public class GetIPBlockByIdHandler : IRequestHandler<GetIPBlockByIdQuery, IPBlockDto?>
{
    private readonly GatewayDbContext _db;
    public GetIPBlockByIdHandler(GatewayDbContext db) => _db = db;

    public async Task<IPBlockDto?> Handle(GetIPBlockByIdQuery request, CancellationToken cancellationToken)
    {
        return await _db.IPBlocks
            .Where(b => b.Id == request.Id)
            .Select(b => new IPBlockDto
            {
                Id = b.Id,
                IPAddress = b.IPAddress,
                Reason = b.Reason,
                CreatedAt = b.CreatedAt,
                ExpiresAt = b.ExpiresAt
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
