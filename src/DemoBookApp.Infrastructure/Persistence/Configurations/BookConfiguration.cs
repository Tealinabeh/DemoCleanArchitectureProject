using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title);
            builder.Property(x => x.Description);
            builder.Property(x => x.Price);
            builder.Property(x => x.DateOfIssue);


            builder.HasOne(x => x.Author)
                    .WithMany(x => x.IssuedBooks);
        }  
    }
}