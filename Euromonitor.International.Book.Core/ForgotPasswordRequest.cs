namespace Euromonitor.International.Book.Core;

public class ForgotPasswordRequest
{
    public string Email {get; set;}

    public string Password {get; set;}

    public string ConfirmPassword {get; set;}
}
