using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.International.Book.Application;
public class SubscriptionService : ISubscriptionService
{
    public Task<bool> CancelSubscriptionAsync(int subscriptionId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateSubscriptionAsync(int userId, int bookId)
    {
        throw new NotImplementedException();
    }
}