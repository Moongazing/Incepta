using Microsoft.EntityFrameworkCore;
using Moongazing.Incepta.Persistence.Context;

namespace Moongazing.Incepta.Infrastructure.IPBlocking;

public class EfIpBlockService : IIPBlockService
{
    private readonly GatewayDbContext _db;

    public EfIpBlockService(GatewayDbContext db)
    {
        _db = db;
    }

    public async Task<bool> IsBlockedAsync(string ip)
    {
        var now = DateTime.UtcNow;
        return await _db.IPBlocks
            .AnyAsync(x => x.IPAddress == ip && (x.ExpiresAt == null || x.ExpiresAt > now));
    }
}
