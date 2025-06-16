using StackExchange.Redis;

namespace Moongazing.Incepta.Infrastructure.Redis;

public class RedisRateLimiter : IRateLimiterService
{
    private readonly IDatabase _redis;

    public RedisRateLimiter(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task<bool> IsRequestAllowedAsync(string key, int limit, int windowSeconds)
    {
        var redisKey = $"ratelimit:{key}";

        var currentCount = await _redis.StringIncrementAsync(redisKey);

        if (currentCount == 1)
        {
            await _redis.KeyExpireAsync(redisKey, TimeSpan.FromSeconds(windowSeconds));
        }

        return currentCount <= limit;
    }
}