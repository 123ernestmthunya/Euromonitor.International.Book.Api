using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application;
public class SubscriptionService : ISubscriptionService
{
    private readonly Db _dbContext;

    public SubscriptionService(Db dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<bool>> CancelSubscriptionAsync(int subscriptionId)
    {
        var subscription = await _dbContext.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionID == subscriptionId);

        if (subscription == null)
        {
            return new Response<bool>(false, ApplicationConstants.SubscriptionNotFound, false);
        }

        _dbContext.Subscriptions.Remove(subscription);
        await _dbContext.SaveChangesAsync();

        return new Response<bool>(true, ApplicationConstants.SubscriptionCancelled, true);
    }

    public async Task<Response<bool>> CreateSubscriptionAsync(int userId, int bookId)
    {
        var user = await _dbContext.Users.Where(user => user.UserID == userId).FirstOrDefaultAsync();

        var book = await _dbContext.Books.Where(user => user.BookEntityID == bookId).FirstOrDefaultAsync();

        if(user == null){

              return new Response<bool>(false, ApplicationConstants.UserNotFound, false);
        }

        if(book == null){

             return new Response<bool>(false, ApplicationConstants.BooNotFound, false);
        }

         var subscription = new SubscriptionEntity
        {
            UserID = userId,
            BookID = bookId,
            StartDate = DateTime.UtcNow,
        };

        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();

         return new Response<bool>(true, ApplicationConstants.SubscibedSuccessful, true);


    }

    public async Task<Response<List<BookSubscriptionResponse>>> getSubscriptionAsync(int userId)
    {
        var subscription = await _dbContext.Subscriptions
            .Where(s => s.UserID == userId)
            .Select(s => new BookSubscriptionResponse{
                 SubscriptionId = s.SubscriptionID,
                 SubscriptionDate = s.StartDate,
                 UnsubscriptionDate = s.EndDate,
                 Book = s.Book
             })
            .ToListAsync();

         return new Response<List<BookSubscriptionResponse>>(true, ApplicationConstants.SubsciptionRetrievedSuccessful, subscription);
      
    }
}