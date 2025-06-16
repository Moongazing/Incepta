namespace Moongazing.Incepta.Infrastructure.Redis;

public interface IRateLimiterService
{
    Task<bool> IsRequestAllowedAsync(string key, int limit, int windowSeconds);
}