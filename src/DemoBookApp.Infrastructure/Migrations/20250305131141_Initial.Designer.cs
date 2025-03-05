﻿// <auto-generated />
using System;
using DemoBookApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoBookApp.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250305131141_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("DemoBookApp.Core.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.PrimitiveCollection<string>("FavoriteAuthors")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.PrimitiveCollection<string>("FavoriteBooks")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DemoBookApp.Core.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT")
                        .HasColumnName("DateOfBirth");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Surname");

                    b.HasKey("Id");

                    b.ToTable("Authors", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            DateOfBirth = new DateOnly(1903, 6, 25),
                            Name = "George",
                            Surname = "Orwell"
                        },
                        new
                        {
                            Id = 2L,
                            DateOfBirth = new DateOnly(1965, 7, 31),
                            Name = "J.K.",
                            Surname = "Rowling"
                        },
                        new
                        {
                            Id = 3L,
                            DateOfBirth = new DateOnly(1892, 1, 3),
                            Name = "J.R.R.",
                            Surname = "Tolkien"
                        },
                        new
                        {
                            Id = 4L,
                            DateOfBirth = new DateOnly(1920, 1, 2),
                            Name = "Isaac",
                            Surname = "Asimov"
                        },
                        new
                        {
                            Id = 5L,
                            DateOfBirth = new DateOnly(1797, 8, 30),
                            Name = "Mary",
                            Surname = "Shelley"
                        },
                        new
                        {
                            Id = 6L,
                            DateOfBirth = new DateOnly(1821, 11, 11),
                            Name = "Fyodor",
                            Surname = "Dostoevsky"
                        });
                });

            modelBuilder.Entity("DemoBookApp.Core.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateOfIssue")
                        .HasColumnType("TEXT")
                        .HasColumnName("DateOfIssue");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT")
                        .HasColumnName("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AuthorId = 1L,
                            DateOfIssue = new DateOnly(1949, 6, 8),
                            Description = "Dystopian novel",
                            Price = 9.99m,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 2L,
                            AuthorId = 1L,
                            DateOfIssue = new DateOnly(1945, 8, 17),
                            Description = "Political satire",
                            Price = 7.99m,
                            Title = "Animal Farm"
                        },
                        new
                        {
                            Id = 3L,
                            AuthorId = 2L,
                            DateOfIssue = new DateOnly(1997, 6, 26),
                            Description = "Fantasy novel",
                            Price = 14.99m,
                            Title = "Harry Potter and the Sorcerer's Stone"
                        },
                        new
                        {
                            Id = 4L,
                            AuthorId = 2L,
                            DateOfIssue = new DateOnly(1998, 7, 2),
                            Description = "Fantasy novel",
                            Price = 15.99m,
                            Title = "Harry Potter and the Chamber of Secrets"
                        },
                        new
                        {
                            Id = 5L,
                            AuthorId = 3L,
                            DateOfIssue = new DateOnly(1937, 9, 21),
                            Description = "Fantasy novel",
                            Price = 12.99m,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 6L,
                            AuthorId = 3L,
                            DateOfIssue = new DateOnly(1954, 7, 29),
                            Description = "Epic fantasy",
                            Price = 25.99m,
                            Title = "The Lord of the Rings"
                        },
                        new
                        {
                            Id = 7L,
                            AuthorId = 4L,
                            DateOfIssue = new DateOnly(1951, 5, 1),
                            Description = "Science fiction",
                            Price = 10.99m,
                            Title = "Foundation"
                        },
                        new
                        {
                            Id = 8L,
                            AuthorId = 4L,
                            DateOfIssue = new DateOnly(1950, 12, 2),
                            Description = "Robot series",
                            Price = 8.99m,
                            Title = "I, Robot"
                        },
                        new
                        {
                            Id = 9L,
                            AuthorId = 5L,
                            DateOfIssue = new DateOnly(1818, 1, 1),
                            Description = "Gothic horror",
                            Price = 6.99m,
                            Title = "Frankenstein"
                        },
                        new
                        {
                            Id = 10L,
                            AuthorId = 6L,
                            DateOfIssue = new DateOnly(1866, 1, 1),
                            Description = "Psychological novel",
                            Price = 11.99m,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 11L,
                            AuthorId = 6L,
                            DateOfIssue = new DateOnly(1880, 11, 1),
                            Description = "Philosophical novel",
                            Price = 13.99m,
                            Title = "The Brothers Karamazov"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c7e9a3a8-4e3c-4c2e-8f3b-1f5c97a6b789",
                            Name = "User",
                            NormalizedName = "USER"
                        },
                        new
                        {
                            Id = "d3f7a8a9-2d1b-46e6-9cbb-4a7e916d2a60",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DemoBookApp.Core.Book", b =>
                {
                    b.HasOne("DemoBookApp.Core.Author", "Author")
                        .WithMany("IssuedBooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DemoBookApp.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DemoBookApp.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DemoBookApp.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DemoBookApp.Core.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DemoBookApp.Core.Author", b =>
                {
                    b.Navigation("IssuedBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
