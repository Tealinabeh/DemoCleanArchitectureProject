namespace DemoBookApp.Core;

public record Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Untitled book";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateOnly DateOfIssue { get; set; }
    public required Guid AuthorId { get; set; } 
    public required Author Author { get; set; } 
}
