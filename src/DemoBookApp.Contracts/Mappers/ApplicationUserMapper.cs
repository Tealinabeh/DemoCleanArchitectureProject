using DemoBookApp.Contracts.Requests;
using DemoBookApp.Core;

namespace DemoBookApp.Contracts.Mappers
{
    public static class ApplicationUserMapper
    {
        public static ApplicationUser ToUser(this RegisterUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
                throw new ArgumentException("UserName is required", nameof(request.UserName));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required", nameof(request.Email));

            return new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email.ToLower(),
            };
        }
        
    }
}