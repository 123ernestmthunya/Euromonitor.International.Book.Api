using Microsoft.Extensions.DependencyInjection;

namespace Euromonitor.International.Book.Application;


public static class ServiceExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
       services.AddTransient<IUserService, UserService>();
       services.AddTransient<ISubscriptionService, SubscriptionService>();
       services.AddTransient<IBookService, BookService>();
       return services;   
    }
    
}
