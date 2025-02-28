namespace DemoBookApp.Core;

public record Book
(
    Guid Id,
    string Title,
    string? Description,
    decimal Price,
    DateOnly DateOfIssue,
    Guid AuthorId ,
    Author Author
);
