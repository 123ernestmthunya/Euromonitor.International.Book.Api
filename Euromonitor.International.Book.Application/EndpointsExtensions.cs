using Microsoft.AspNetCore.Builder;
using Euromonitor.International.Book.Core;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

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
        })
        .WithName("Register")
        .AllowAnonymous();
        
        // Login 
        app.MapPost("/login", async (LoginRequest loginRequest, IUserService userService) =>
        {
            
            var response = await userService.LoginUserAsync(loginRequest);
            return response.IsSuccess 
                ? Results.Ok(new { message = response.Message,Success = response.IsSuccess, user = response.Data}) 
                : Results.BadRequest(response.Message);
        })
        .WithName("Login")
        .AllowAnonymous();

        // Login 
        app.MapPost("/susbribe", async (HttpContext context ,SubscribeRequest request, ISubscriptionService subscribeService) =>
        {
            if (!(context.User.Identity?.IsAuthenticated ?? false))
            {
                return Results.Unauthorized();
            }

            var response = await subscribeService.CreateSubscriptionAsync(request.UserID, request.BookID);
            return response.IsSuccess 
                ? Results.Ok(new { message = response.Message,Success = response.IsSuccess}) 
                : Results.BadRequest(response.Message);
        })
        .WithName("susbribe")
        .RequireAuthorization();

        app.MapGet("/subscriptions/{userId}", async (HttpContext context, int userId, ISubscriptionService subscribeService) =>
        {
          if (!(context.User.Identity?.IsAuthenticated ?? false))
          {
             return Results.Unauthorized();
          }

          var response = await subscribeService.getSubscriptionAsync(userId);
          return response.IsSuccess && response.Data.Count > 0
                ? Results.Ok(new {Success = response.IsSuccess, response.Data}) 
                : Results.BadRequest(ApplicationConstants.SubscriptionNotFound);
        })
        .WithName("subscriptions")
        .RequireAuthorization();

        app.MapDelete("/cancel-subscription/{subscriptionId}", async (HttpContext context, int subscriptionId, ISubscriptionService subscribeService) =>
        {
            if (!(context.User.Identity?.IsAuthenticated ?? false))
            {
             return Results.Unauthorized();
            }
            var response = await subscribeService.CancelSubscriptionAsync(subscriptionId);
            return response.IsSuccess
                ? Results.Ok(new { Success = response.IsSuccess, subscriptions = response.Data })
                : Results.BadRequest(ApplicationConstants.SubscriptionNotFound);
        })
        .WithName("cancel-subscription")
        .RequireAuthorization();
        

        app.MapPost("/password-reset", async (ForgotPasswordRequest request, IUserService userService) =>
        {
            var response = await userService.ForgotPasswordAsync(request);
            return response.IsSuccess
                ? Results.Ok(new { Success = response.IsSuccess, Message = response.Message})
                : Results.BadRequest(response.Message);
        })
        .WithName("password-reset")
        .AllowAnonymous();

        app.MapGet("/books", async (IBookService bookService) =>
        {
           var books = await bookService.GetBooks();
           return Results.Ok(books);
        })
        .WithName("books")
        .AllowAnonymous();

       return app;
     }
}
