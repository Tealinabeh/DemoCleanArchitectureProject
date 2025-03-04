using System.Dynamic;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Infrastructure.Persistence
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : DbContext(options)
    {
        override protected void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthorConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());

            SeedAuthors(builder);
            SeedBooks(builder);
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

        private void SeedBooks(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "1984", Description = "Dystopian novel", Price = 9.99m, DateOfIssue = new DateOnly(1949, 6, 8), AuthorId = 1},
                new Book { Id = 2, Title = "Animal Farm", Description = "Political satire", Price = 7.99m, DateOfIssue = new DateOnly(1945, 8, 17), AuthorId = 1 },
                new Book { Id = 3, Title = "Harry Potter and the Sorcerer's Stone", Description = "Fantasy novel", Price = 14.99m, DateOfIssue = new DateOnly(1997, 6, 26), AuthorId = 2},
                new Book { Id = 4, Title = "Harry Potter and the Chamber of Secrets", Description = "Fantasy novel", Price = 15.99m, DateOfIssue = new DateOnly(1998, 7, 2), AuthorId = 2},
                new Book { Id = 5, Title = "The Hobbit", Description = "Fantasy novel", Price = 12.99m, DateOfIssue = new DateOnly(1937, 9, 21), AuthorId = 3},
                new Book { Id = 6, Title = "The Lord of the Rings", Description = "Epic fantasy", Price = 25.99m, DateOfIssue = new DateOnly(1954, 7, 29), AuthorId = 3},
                new Book { Id = 7, Title = "Foundation", Description = "Science fiction", Price = 10.99m, DateOfIssue = new DateOnly(1951, 5, 1), AuthorId = 4},
                new Book { Id = 8, Title = "I, Robot", Description = "Robot series", Price = 8.99m, DateOfIssue = new DateOnly(1950, 12, 2), AuthorId = 4},
                new Book { Id = 9, Title = "Frankenstein", Description = "Gothic horror", Price = 6.99m, DateOfIssue = new DateOnly(1818, 1, 1), AuthorId = 5},
                new Book { Id = 10, Title = "Crime and Punishment", Description = "Psychological novel", Price = 11.99m, DateOfIssue = new DateOnly(1866, 1, 1), AuthorId = 6},
                new Book { Id = 11, Title = "The Brothers Karamazov", Description = "Philosophical novel", Price = 13.99m, DateOfIssue = new DateOnly(1880, 11, 1), AuthorId = 6}
            );
        }
    }
}