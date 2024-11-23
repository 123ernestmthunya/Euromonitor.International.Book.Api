namespace Euromonitor.International.Book.Core;

public class RegisterResponse
{
    public string Email { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int UserId { get; set; } = default!;
}
