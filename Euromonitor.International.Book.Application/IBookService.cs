using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application;

public interface IBookService
{
    Task<List<BookEntity>> GetBooks();
}
