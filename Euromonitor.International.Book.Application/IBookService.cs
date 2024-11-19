namespace Euromonitor.International.Book.Application;

public interface IBookService
{
    Task<List<Euromonitor.International.Book.Core.Book>> GetBooks();
}
