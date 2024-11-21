

using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application;

public interface ISubscriptionService
{
    Task<Response<bool>> CreateSubscriptionAsync(int userId, int bookId);
    Task<Response<bool>> CancelSubscriptionAsync(int subscriptionId);
    Task<Response<List<BookSubscriptionResponse>>> getSubscriptionAsync(int userId);
} 