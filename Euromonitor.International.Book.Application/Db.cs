using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.Application;

public class Db : DbContext
{
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<RegisterEntity> Users { get; set; }
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegisterEntity>(entity =>
        {
            entity.HasKey(u => u.UserID); 
        });

         modelBuilder.Entity<SubscriptionEntity>(entity =>
    {
        entity.HasKey(s => s.SubscriptionID); // Primary Key

        entity.HasOne(s => s.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(s => s.Book)
            .WithMany(b => b.Subscriptions)
            .HasForeignKey(s => s.BookID)
            .OnDelete(DeleteBehavior.Cascade);
    });
        modelBuilder.Entity<BookEntity>(entity =>
        {
            entity.HasKey(b => b.BookEntityID);
        });
    }
}
