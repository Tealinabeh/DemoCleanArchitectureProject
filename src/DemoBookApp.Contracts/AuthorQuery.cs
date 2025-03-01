using DemoBookApp.Core;

namespace DemoBookApp.Contracts;

public record AuthorQuery(
    string? Name,
    string? Surname
);