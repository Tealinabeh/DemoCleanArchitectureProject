using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts;

public record AuthorQuery(
    string? Name,
    string? Surname,
    string? OrderBy,
    bool IsDescending,
    [Required, Range(0, 900000)] int PageNumber = 1,
    [Required, Range(0, 900000)] int PageSize = 20
);