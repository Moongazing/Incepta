using StackExchange.Redis;

namespace Moongazing.Incepta.Infrastructure.IPBlocking;

public class RedisIpBlockService : IIPBlockService
{
    private readonly IDatabase _redis;

    public RedisIpBlockService(IConnectionMultiplexer muxer)
    {
        _redis = muxer.GetDatabase();
    }

    public async Task<bool> IsBlockedAsync(string ip)
    {
        return await _redis.KeyExistsAsync($"ipblock:{ip}");
    }
}
