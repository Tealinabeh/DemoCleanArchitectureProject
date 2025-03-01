using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        private const int NameMaxLength = 50;
        private const int SurnameMaxLength = 50;
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(NameMaxLength)
                    .HasColumnName("Name");

            builder.Property(x => x.Surname)
                    .IsRequired()
                    .HasMaxLength(SurnameMaxLength)
                    .HasColumnName("Surname");

            builder.Property(x => x.DateOfBirth)
                    .IsRequired()
                    .HasColumnName("Date of birth");

            builder.HasMany(x => x.IssuedBooks)
                    .WithOne(x => x.Author)
                    .HasConstraintName("Issued books");
        }  
    }
}