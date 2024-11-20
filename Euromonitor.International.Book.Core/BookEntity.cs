using System.ComponentModel.DataAnnotations;

namespace Euromonitor.International.Book.Core;

// Decide to add image to the BookEntity class to holder the actual image of
// the book for displaying purposes
public class BookEntity
{
    [Key]
    public int BookEntityID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    public string imagePath {get; set;}
    public ICollection<SubscriptionEntity> Subscriptions { get; set; } = new List<SubscriptionEntity>();
}