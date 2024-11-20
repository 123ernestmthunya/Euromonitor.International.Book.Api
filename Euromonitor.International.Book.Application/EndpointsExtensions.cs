using Microsoft.AspNetCore.Builder;
using Euromonitor.International.Book.Core;
using Microsoft.AspNetCore.Http;

namespace Euromonitor.International.Book.Application;
public static class EndpointsExtensions
{
     public static WebApplication MapEndpoints(this WebApplication app)
     { 
        // Register 
        app.MapPost("/users/register", async (RegisterRequest user, IUserService userService) =>
        {
             var response = await userService.RegisterUserAsync(user);
             return Results.Ok(new { message = response.Message,Success = response.IsSuccess, user = response.Data });
        }).WithName("Register");
        
        // Login 
        app.MapPost("/login", async (LoginRequest loginRequest, IUserService userService) =>
        {
            
            var response = await userService.LoginUserAsync(loginRequest);
            return response.IsSuccess 
                ? Results.Ok(new { message = response.Message,Success = response.IsSuccess, user = response.Data}) 
                : Results.BadRequest(response.Message);
        }).WithName("Login");

        // Login 
        app.MapPost("/susbribe", async (SubscribeRequest request, ISubscriptionService subscribeService) =>
        {
            
            var response = await subscribeService.CreateSubscriptionAsync(request.UserID, request.BookID);
            return response.IsSuccess 
                ? Results.Ok(new { message = response.Message,Success = response.IsSuccess}) 
                : Results.BadRequest(response.Message);
        }).WithName("susbribe");

        app.MapGet("/subscriptions/{userId}", async (int userId, ISubscriptionService subscribeService) =>
        {
          var response = await subscribeService.getSubscriptionAsync(userId);
          return response.IsSuccess && response.Data.Count > 0
                ? Results.Ok(new {Success = response.IsSuccess, subscriptions = response.Data}) 
                : Results.BadRequest(ApplicationConstants.SubscriptionNotFound);
        }).WithName("subscriptions");

        app.MapGet("/books", async (IBookService bookService) =>
        {
           var books = await bookService.GetBooks();
           return Results.Ok(books);
        }).WithName("books");;

       return app;
     }
}
