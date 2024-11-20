using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.Application;

public class Db : DbContext
{
    public DbSet<Euromonitor.International.Book.Core.Book> Books { get; set; }
    public DbSet<RegisterEntity> Users { get; set; }
    // public DbSet<Subscription> Subscriptions { get; set; }
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegisterEntity>()
            .HasKey(u => u.UserID);
    }
}
