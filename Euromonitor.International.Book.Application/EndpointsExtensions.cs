using Microsoft.AspNetCore.Builder;
using Euromonitor.International.Book.Core;
using Microsoft.AspNetCore.Http;

namespace Euromonitor.International.Book.Application;
public static class EndpointsExtensions
{
     public static WebApplication MapEndpoints(this WebApplication app)
     { 
        // app.MapPost("/users/register", async (User user, IUserService userService) =>
        // {
        //     var createdUser = await userService.RegisterUserAsync(user);

        // }).WithName("Register");
        

        // app.MapPost("/users/login", async (User request, IUserService userService) =>
        // {
        //     var user = await userService.LoginUserAsync(request.Email, request.Password);
            
        // }).WithName("Login");

        app.MapGet("/books", async (IBookService bookService) =>
        {
           var books = await bookService.GetBooks();
           return Results.Ok(books);
        });

    //     app.MapDelete("/subscriptions/{subscriptionId}", async (int subscriptionId, ISubscriptionService subscriptionService) =>
    //     {
    //         await subscriptionService.CancelSubscriptionAsync(subscriptionId);
    //     }).WithName("Subscription");;

       return app;
     }
}
