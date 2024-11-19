using Microsoft.AspNetCore.Builder;
using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application;
public static class EndpointsExtensions
{
     public static WebApplication MapEndpoints(this WebApplication app)
     { 
        app.MapPost("/users/register", async (User user, IUserService userService) =>
        {
            var createdUser = await userService.RegisterUserAsync(user);

        }).WithName("Register");
        

        app.MapPost("/users/login", async (User request, IUserService userService) =>
        {
            var user = await userService.LoginUserAsync(request.Email, request.Password);
            
        }).WithName("Login");

        // app.MapPost("/subscriptions", async (SubscriptionRequest request, ISubscriptionService subscriptionService) =>
        // {
        //     var subscription = await subscriptionService.CreateSubscriptionAsync(request.UserId, request.BookId);
        //     return Results.Created($"/subscriptions/{subscription.SubscriptionId}", subscription);
        // });

        app.MapDelete("/subscriptions/{subscriptionId}", async (int subscriptionId, ISubscriptionService subscriptionService) =>
        {
            await subscriptionService.CancelSubscriptionAsync(subscriptionId);
        }).WithName("Subscription");;

       return app;
     }
}
