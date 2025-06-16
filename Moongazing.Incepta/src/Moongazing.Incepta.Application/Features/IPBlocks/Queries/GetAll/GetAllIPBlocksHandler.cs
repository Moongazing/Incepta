using MediatR;
using Moongazing.Incepta.Application.Features.IPBlocks.Dtos;
using Moongazing.Incepta.Application.Features.IPBlocks.Queries.GetAll;
using Moongazing.Incepta.Persistence.Context;

public class GetAllIPBlocksHandler : IRequestHandler<GetAllIPBlocksQuery, List<IPBlockDto>>
{
    private readonly GatewayDbContext _db;
    public GetAllIPBlocksHandler(GatewayDbContext db) => _db = db;

    public async Task<List<IPBlockDto>> Handle(GetAllIPBlocksQuery request, CancellationToken cancellationToken)
    {
        return await _db.IPBlocks
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new IPBlockDto
            {
                Id = b.Id,
                IPAddress = b.IPAddress,
                Reason = b.Reason,
                CreatedAt = b.CreatedAt,
                ExpiresAt = b.ExpiresAt
            })
            .ToListAsync(cancellationToken);
    }
}
