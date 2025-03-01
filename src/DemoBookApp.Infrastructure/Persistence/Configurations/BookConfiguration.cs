using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {

        private const int TitleMaxLength = 70;
        private const int DescriptionMaxLength = 200;

        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(TitleMaxLength)
                    .HasColumnName("Title");

            builder.Property(x => x.Description)
                    .HasMaxLength(DescriptionMaxLength)
                    .HasColumnName("Description");

            builder.Property(x => x.Price)
                    .HasColumnName("Price");
            builder.Property(x => x.DateOfIssue)
                    .IsRequired()
                    .HasColumnName("Date of issue");


            builder.HasOne(x => x.Author)
                    .WithMany(x => x.IssuedBooks)
                    .HasForeignKey(x => x.AuthorId)
                    .HasConstraintName("Author");
        }  
    }
}