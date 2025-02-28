using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Persistence.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder){
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Surname);
            builder.Property(x => x.DateOfBirth);


            builder.HasMany(x => x.IssuedBooks)
                    .WithOne(x => x.Author)
                    .HasForeignKey(x => x.AuthorId);
        }  
    }
}