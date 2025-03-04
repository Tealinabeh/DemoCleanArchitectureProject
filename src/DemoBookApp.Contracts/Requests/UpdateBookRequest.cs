using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record UpdateBookRequest
    (
        [Required, MinLength(3), MaxLength(70)] string Title,
        [Required, MaxLength(200)] string Description,
        [Required, Range(0, 2000)] decimal Price,
        [Required] DateOnly DateOfIssue,
        [Required] long AuthorId
    );
}