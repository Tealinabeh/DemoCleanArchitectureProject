using System.ComponentModel.DataAnnotations;
using DemoBookApp.Core;

namespace DemoBookApp.Contracts.Requests
{
    public record RegisterUserRequest
    (
        [Required] string UserName,
        [Required, EmailAddress] string Email,
        [Required, MinLength(12)] string Password
    );
}