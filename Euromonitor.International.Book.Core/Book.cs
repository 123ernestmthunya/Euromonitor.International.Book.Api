namespace Euromonitor.International.Book.Core;

// Decide to add image to the Book class to holder the actual image of
// the book for displaying purposes
public record Book(int BookID,string Name, string Description, decimal Price, string Image, int Quantity);