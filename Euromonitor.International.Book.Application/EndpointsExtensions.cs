using Microsoft.AspNetCore.Builder;
using Euromonitor.International.Book.Core;
using Microsoft.AspNetCore.Http;

namespace Euromonitor.International.Book.Application;
public static class EndpointsExtensions
{
     public static WebApplication MapEndpoints(this WebApplication app)
     { 
        app.MapPost("/users/register", async (RegisterRequest user, IUserService userService) =>
        {
            await userService.RegisterUserAsync(user);

        }).WithName("Register");
        
        app.MapPost("/login", async (LoginRequest loginRequest, IUserService userService) =>
        {
         var isSuccess = await userService.LoginUserAsync(loginRequest);

         if (isSuccess)
         {
            return Results.Ok("Login successful");
         }
         else
         {
            return Results.BadRequest("Invalid username or password");
         }
         }).WithName("Login");

        app.MapGet("/books", async (IBookService bookService) =>
        {
           var books = await bookService.GetBooks();
           return Results.Ok(books);
        });

       return app;
     }
}
