using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts.Requests
{
    public record ChangeRoleRequest
    (
        [Required, EmailAddress]string Email,
        [Required] string RoleName,
        [Required] ChangeRoleOperation Operation
    );
    public enum ChangeRoleOperation
    {
        Add = 1,
        Remove = 2,
    }
}