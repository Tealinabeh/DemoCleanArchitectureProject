using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record RegisterUserRequest
    (
        [Required] string UserName,
        [Required, EmailAddress] string Email,
        [Required, MinLength(12)] string Password
    );
}