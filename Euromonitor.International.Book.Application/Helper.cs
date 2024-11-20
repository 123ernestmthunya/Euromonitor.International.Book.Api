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
    public static List<Euromonitor.International.Book.Core.Book> GetDummyBooks()
    {
        return new List<Euromonitor.International.Book.Core.Book>
        {
            new Core.Book(1, "The Great Gatsby", "A classic novel by F. Scott Fitzgerald", 10.99M, "greatgatsby.jpg", 100),
            new Core.Book(2, "1984", "A dystopian novel by George Orwell", 8.99M, "1984.jpg", 150),
            new Core.Book(3, "To Kill a Mockingbird", "A novel by Harper Lee about racial injustice", 12.99M, "mockingbird.jpg", 200),
            new Core.Book(4, "Pride and Prejudice", "A romantic novel by Jane Austen", 9.99M, "prideandprejudice.jpg", 120)
        };
    }
}
