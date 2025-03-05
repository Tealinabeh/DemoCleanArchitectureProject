using Microsoft.AspNetCore.Identity;

namespace DemoBookApp.Core
{
    public class ApplicationUser : IdentityUser
    {
        public List<long> FavoriteBooks { get; set; } = new();
        public List<long> FavoriteAuthors { get; set; } = new();
    }
}