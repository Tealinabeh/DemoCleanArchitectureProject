namespace DemoBookApp.Core;

public class Book
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public DateOnly DateOfIssue { get; set; }
    public long AuthorId { get; set; }
    public Author Author { get; set; } = new Author();
}
