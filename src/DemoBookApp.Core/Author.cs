namespace DemoBookApp.Core;

public record Author(
    Guid Id,
    string Name,
    string Surname,
    DateOnly DateOfBirth,
    List<Book>? IssuedBooks
);
