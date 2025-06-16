namespace Moongazing.Incepta.Infrastructure.IPBlocking;

public interface IIPBlockService
{
    Task<bool> IsBlockedAsync(string ip);
}
