using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                    .IsRequired()
                    .HasColumnName("Title");

            builder.Property(x => x.Description)
                    .HasColumnName("Description");

            builder.Property(x => x.Price)
                    .HasColumnName("Price")
                    .HasPrecision(18, 2);

            builder.Property(x => x.DateOfIssue)
                    .IsRequired()
                    .HasColumnName("DateOfIssue");


            builder.HasOne(x => x.Author)
                    .WithMany(x => x.IssuedBooks)
                    .HasForeignKey(x => x.AuthorId)
                    .IsRequired();
        }  
    }
}