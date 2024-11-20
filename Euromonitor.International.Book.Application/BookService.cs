
using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.Application;

public class BookService : IBookService
{
    private readonly Db _dbContext;

    public BookService(Db dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<BookEntity>>  GetBooks()
    {
        if (!_dbContext.Books.Any())
        {
            await _dbContext.Books.AddRangeAsync(Helper.BookEntitySeed());
            await _dbContext.SaveChangesAsync();
        }

        return await _dbContext.Books.ToListAsync();
    }
}

