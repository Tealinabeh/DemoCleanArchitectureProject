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
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}