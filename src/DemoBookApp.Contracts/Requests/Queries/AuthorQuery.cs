namespace DemoBookApp.Contracts;

public record AuthorQuery(
    string? Name,
    string? Surname,
    string? OrderBy,
    bool IsDescending,
    int PageNumber = 1,
    int PageSize = 20
);