using System.ComponentModel.DataAnnotations;

namespace Euromonitor.International.Book.Core;

public class RegisterEntity : User
{
    [Key]
    public int UserID { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
