using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record UpdateBookRequest
    (
        [MinLength(3), MaxLength(70)]string Title,
        [MaxLength(200)] string Description,
        [Range(0, 2000)] decimal Price,
        DateOnly DateOfIssue,
        long AuthorId
    );
}