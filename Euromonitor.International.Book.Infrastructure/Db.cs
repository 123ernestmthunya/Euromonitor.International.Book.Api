using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.Infrastructure;

public class Db : DbContext
{
    public DbSet<Euromonitor.International.Book.Core.Book> Books { get; set; }
    // public DbSet<User> Users { get; set; }
    // public DbSet<Subscription> Subscriptions { get; set; }
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }
}
