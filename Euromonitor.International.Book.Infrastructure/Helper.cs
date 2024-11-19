namespace Euromonitor.International.Book.Infrastructure;

public static class Helper
{
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
