namespace DemoBookApp.Contracts;

public record AuthorQuery(
    string? Name,
    string? Surname,
    string? SortBy,
    bool IsDescending,
    int PageNumber = 1,
    int PageSize = 20
);