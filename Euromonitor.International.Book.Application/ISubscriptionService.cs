

namespace Euromonitor.International.Book.Application;

public interface ISubscriptionService
{
    Task<bool> CreateSubscriptionAsync(int userId, int bookId);
    Task<bool> CancelSubscriptionAsync(int subscriptionId);
} 