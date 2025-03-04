using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record UpdateAuthorRequest
    (
        [Required, MinLength(2), MaxLength(50)] string Name,
        [Required, MinLength(2), MaxLength(50)] string Surname,
        [Required] DateOnly DateOfBirth
    );
}