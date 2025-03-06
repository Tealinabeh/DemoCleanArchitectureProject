using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Infrastructure.Persistence
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        private const string UserRoleId = "c7e9a3a8-4e3c-4c2e-8f3b-1f5c97a6b789";
        private const string AdminRoleId = "d3f7a8a9-2d1b-46e6-9cbb-4a7e916d2a60";
        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());

            SeedAuthors(builder);
            SeedBooks(builder);
            SeedRoles(builder);
            SeedAdmin(builder);
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        private void SeedAuthors(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData
            (
                new Author { Id = 1, Name = "George", Surname = "Orwell", DateOfBirth = new DateOnly(1903, 6, 25) },
                new Author { Id = 2, Name = "J.K.", Surname = "Rowling", DateOfBirth = new DateOnly(1965, 7, 31) },
                new Author { Id = 3, Name = "J.R.R.", Surname = "Tolkien", DateOfBirth = new DateOnly(1892, 1, 3) },
                new Author { Id = 4, Name = "Isaac", Surname = "Asimov", DateOfBirth = new DateOnly(1920, 1, 2) },
                new Author { Id = 5, Name = "Mary", Surname = "Shelley", DateOfBirth = new DateOnly(1797, 8, 30) },
                new Author { Id = 6, Name = "Fyodor", Surname = "Dostoevsky", DateOfBirth = new DateOnly(1821, 11, 11) }
            );
        }

        private static void SeedBooks(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Description = "Dystopian novel", Price = 9.99m, DateOfIssue = new DateOnly(1949, 6, 8), AuthorId = 1 },
                new Book { Id = 2, Title = "Animal Farm", Description = "Political satire", Price = 7.99m, DateOfIssue = new DateOnly(1945, 8, 17), AuthorId = 1 },
                new Book { Id = 3, Title = "Harry Potter and the Sorcerer's Stone", Description = "Fantasy novel", Price = 14.99m, DateOfIssue = new DateOnly(1997, 6, 26), AuthorId = 2 },
                new Book { Id = 4, Title = "Harry Potter and the Chamber of Secrets", Description = "Fantasy novel", Price = 15.99m, DateOfIssue = new DateOnly(1998, 7, 2), AuthorId = 2 },
                new Book { Id = 5, Title = "The Hobbit", Description = "Fantasy novel", Price = 12.99m, DateOfIssue = new DateOnly(1937, 9, 21), AuthorId = 3 },
                new Book { Id = 6, Title = "The Lord of the Rings", Description = "Epic fantasy", Price = 25.99m, DateOfIssue = new DateOnly(1954, 7, 29), AuthorId = 3 },
                new Book { Id = 7, Title = "Foundation", Description = "Science fiction", Price = 10.99m, DateOfIssue = new DateOnly(1951, 5, 1), AuthorId = 4 },
                new Book { Id = 8, Title = "I, Robot", Description = "Robot series", Price = 8.99m, DateOfIssue = new DateOnly(1950, 12, 2), AuthorId = 4 },
                new Book { Id = 9, Title = "Frankenstein", Description = "Gothic horror", Price = 6.99m, DateOfIssue = new DateOnly(1818, 1, 1), AuthorId = 5 },
                new Book { Id = 10, Title = "Crime and Punishment", Description = "Psychological novel", Price = 11.99m, DateOfIssue = new DateOnly(1866, 1, 1), AuthorId = 6 },
                new Book { Id = 11, Title = "The Brothers Karamazov", Description = "Philosophical novel", Price = 13.99m, DateOfIssue = new DateOnly(1880, 11, 1), AuthorId = 6 }
            );
        }
        private void SeedRoles(ModelBuilder builder)
        {
            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Id = UserRoleId,
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
        private void SeedAdmin(ModelBuilder builder)
        {
            var idString = "3f8a9c6e-5b74-4b8e-a123-1d5c9f6e7a8b";
            var securityStamp = "b1d4e8c2-7f6a-41d2-8e9c-3a6f7b4d2e1f";
            var concurrencyStamp = "2e5b7c3d-4f1a-9d8e-6a2f-3b7c1e4d5a9f";

#pragma warning disable CS0219 

            var password = "Supersecurepassword=123";
                              //Hardcoded from PasswordHasher<ApplicationUser>().HashPassword(null, "password")
            var passwordHash = "AQAAAAIAAYagAAAAEPkOEYBf3FFtpqJ6E/T+fXsa5fZkqs0P2Me6of4k6qTKJ/GTuXY5E4QIJ61g3aK7Eg==";

#pragma warning restore CS0219 

            var adminUser = new ApplicationUser
            {
                Id = idString,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@demoapp.com",
                NormalizedEmail = "ADMIN@DEMOAPP.COM",
                EmailConfirmed = true,
                PasswordHash = passwordHash,
                SecurityStamp = securityStamp,
                ConcurrencyStamp = concurrencyStamp,
            };
            builder.Entity<ApplicationUser>().HasData(adminUser);

            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = idString,
                RoleId = AdminRoleId
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);
        }
    }
}