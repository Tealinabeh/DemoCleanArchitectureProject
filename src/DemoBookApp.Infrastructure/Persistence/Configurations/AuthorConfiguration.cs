using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                    .IsRequired()
                    .HasColumnName("Name");

            builder.Property(x => x.Surname)
                    .IsRequired()
                    .HasColumnName("Surname");

            builder.Property(x => x.DateOfBirth)
                    .IsRequired()
                    .HasColumnName("DateOfBirth");

            builder.HasMany(x => x.IssuedBooks)
                    .WithOne(x => x.Author)     
                    .IsRequired();
        }  
    }
}