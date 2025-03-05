using DemoBookApp.Core;

namespace DemoBookApp.Application.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}