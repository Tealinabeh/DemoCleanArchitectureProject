using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record LogInUserRequest
    (
        [Required, EmailAddress] string Email,
        [Required, MinLength(12)] string Password
    );
}