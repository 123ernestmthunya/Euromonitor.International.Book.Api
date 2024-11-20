using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application;

public static class Helper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    public static List<BookEntity> BookEntitySeed()
    {
        return new List<BookEntity>
        {
            new BookEntity {
                BookEntityID = 1,
                Name = "The Great Gatsby",
                Description = "A classic novel by F. Scott Fitzgerald",
                Price = 10.99M,
                imagePath = "greatgatsby.jpg",
            },
            new BookEntity {
                BookEntityID = 2,
                Name = "The Great Gatsby",
                Description = "A classic novel by F. Scott Fitzgerald",
                Price = 10.99M,
                imagePath = "greatgatsby.jpg",
            }
        };

    }
}
