using Microsoft.EntityFrameworkCore;
using Xunit;
using Euromonitor.International.Book.Application;
using Euromonitor.International.Book.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Euromonitor.International.Book.Tests
{
    public class SubscriptionServiceTests
    {
        private readonly DbContextOptions<Db> _dbContextOptions;

        public SubscriptionServiceTests()
        {
            // Set up In-Memory database for each test
            _dbContextOptions = new DbContextOptionsBuilder<Db>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        private SubscriptionService CreateService()
        {
            var dbContext = new Db(_dbContextOptions); // Use In-Memory DbContext
            return new SubscriptionService(dbContext);
        }

        [Fact]
        public async Task CreateSubscriptionAsync_UserAndBookExist_ReturnsSuccess()
        {
            // Arrange
            var userId = 1;
            var bookId = 1;
            var user = new RegisterEntity { UserID = userId, Email = "test@example.com", FirstName = "John", LastName = "Doe" , PasswordHash = null, PasswordSalt = null};
            var book = new BookEntity { BookEntityID = bookId};
            var subscriptionService = CreateService();

            // Add test data to In-Memory DB
            using (var context = new Db(_dbContextOptions))
            {
                context.Users.Add(user);
                context.Books.Add(book);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await subscriptionService.CreateSubscriptionAsync(userId, bookId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ApplicationConstants.SubscibedSuccessful, result.Message);
        }

        [Fact]
        public async Task CreateSubscriptionAsync_UserNotFound_ReturnsFailure()
        {
            // Arrange
            var userId = 1;
            var bookId = 1;
            var subscriptionService = CreateService();

            // Act
            var result = await subscriptionService.CreateSubscriptionAsync(userId, bookId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ApplicationConstants.UserNotFound, result.Message);
        }

        [Fact]
        public async Task CreateSubscriptionAsync_BookNotFound_ReturnsFailure()
        {
            // Arrange
            var userId = 1;
            var bookId = 1;
            var user = new RegisterEntity { UserID = userId, Email = "test@example.com", FirstName = "John", LastName = "Doe" , PasswordHash = null, PasswordSalt = null};
            var subscriptionService = CreateService();

            // Add test data (only the user, no book)
            using (var context = new Db(_dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await subscriptionService.CreateSubscriptionAsync(userId, bookId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ApplicationConstants.BooNotFound, result.Message);
        }

        [Fact]
        public async Task GetSubscriptionAsync_UserHasSubscriptions_ReturnsList()
        {
            // Arrange
            var userId = 1;
            var bookId1 = 1;
            var bookId2 = 2;
            var user = new RegisterEntity { UserID = userId, Email = "test@example.com", FirstName = "John", LastName = "Doe" , PasswordHash = null, PasswordSalt = null};
            var book1 = new BookEntity { BookEntityID = bookId1};
            var book2 = new BookEntity { BookEntityID = bookId2};
            var subscriptionService = CreateService();

            // Add test data to In-Memory DB
            using (var context = new Db(_dbContextOptions))
            {
                context.Users.Add(user);
                context.Books.AddRange(book1, book2);
                context.SaveChanges();

                var subscription1 = new SubscriptionEntity { UserID = userId, BookID = bookId1, StartDate = DateTime.UtcNow };
                var subscription2 = new SubscriptionEntity { UserID = userId, BookID = bookId2, StartDate = DateTime.UtcNow };
                context.Subscriptions.AddRange(subscription1, subscription2);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await subscriptionService.getSubscriptionAsync(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ApplicationConstants.SubscibedSuccessful, result.Message);
            Assert.Equal(2, result.Data.Count); // Should return both subscriptions
        }

        [Fact]
        public async Task GetSubscriptionAsync_NoSubscriptions_ReturnsEmptyList()
        {
            // Arrange
            var userId = 1;
            var subscriptionService = CreateService();

            // Add user data but no subscriptions
            using (var context = new Db(_dbContextOptions))
            {
                var user = new RegisterEntity { UserID = userId, Email = "test@example.com", FirstName = "John", LastName = "Doe" , PasswordHash = null, PasswordSalt = null};
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await subscriptionService.getSubscriptionAsync(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(ApplicationConstants.SubscibedSuccessful, result.Message);
            Assert.Empty(result.Data); // No subscriptions should be returned
        }
    }
}
