using Euromonitor.International.Book.Application;
using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.UnitTests;

public class AuthenticationUnitTests
{
     private readonly DbContextOptions<Db> _dbContextOptions;
     
     public AuthenticationUnitTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<Db>()
            .UseInMemoryDatabase(databaseName: "UserServiceTestDb")
            .Options;
    }
     private Db CreateDbContext()
    {
        return new Db(_dbContextOptions);
    }
    
    [Fact]
    public async Task LoginUserAsync_UserExistsAndPasswordMatches_ReturnsSuccessResponse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var userService = new UserService(dbContext);

        var password = "TestPassword";
        Helper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        var user = new RegisterEntity
        {
            Email = "test@example.com",
            FirstName = "Test",
            LastName = "User",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = password
        };

        // Act
        var result = await userService.LoginUserAsync(loginRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ApplicationConstants.LoginSuccessful, result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal("test@example.com", result.Data.Email);
    }

    [Fact]
    public async Task LoginUserAsync_UserDoesNotExist_ReturnsFailedResponse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var userService = new UserService(dbContext);

        var loginRequest = new LoginRequest
        {
            Email = "nonexistent@example.com",
            Password = "password"
        };

        // Act
        var result = await userService.LoginUserAsync(loginRequest);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(ApplicationConstants.LoginFailed, result.Message);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task RegisterUserAsync_ValidRequest_SavesUserAndReturnsSuccessResponse()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var userService = new UserService(dbContext);

        var registerRequest = new RegisterRequest
        {
            Email = "newuser@example.com",
            FirstName = "New",
            LastName = "User",
            Password = "NewPassword"
        };

        // Act
        var result = await userService.RegisterUserAsync(registerRequest);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ApplicationConstants.RegistrationSuccessful, result.Message);
        Assert.NotNull(result.Data);

        var savedUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == registerRequest.Email);
        Assert.NotNull(savedUser);
        Assert.Equal(registerRequest.Email, savedUser.Email);
        Assert.NotNull(savedUser.PasswordHash);
        Assert.NotNull(savedUser.PasswordSalt);
    }
}