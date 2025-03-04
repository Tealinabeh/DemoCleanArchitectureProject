using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoBookApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DateOfIssue = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Name", "Surname" },
                values: new object[,]
                {
                    { 1L, new DateOnly(1903, 6, 25), "George", "Orwell" },
                    { 2L, new DateOnly(1965, 7, 31), "J.K.", "Rowling" },
                    { 3L, new DateOnly(1892, 1, 3), "J.R.R.", "Tolkien" },
                    { 4L, new DateOnly(1920, 1, 2), "Isaac", "Asimov" },
                    { 5L, new DateOnly(1797, 8, 30), "Mary", "Shelley" },
                    { 6L, new DateOnly(1821, 11, 11), "Fyodor", "Dostoevsky" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "DateOfIssue", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1L, 1L, new DateOnly(1949, 6, 8), "Dystopian novel", 9.99m, "1984" },
                    { 2L, 1L, new DateOnly(1945, 8, 17), "Political satire", 7.99m, "Animal Farm" },
                    { 3L, 2L, new DateOnly(1997, 6, 26), "Fantasy novel", 14.99m, "Harry Potter and the Sorcerer's Stone" },
                    { 4L, 2L, new DateOnly(1998, 7, 2), "Fantasy novel", 15.99m, "Harry Potter and the Chamber of Secrets" },
                    { 5L, 3L, new DateOnly(1937, 9, 21), "Fantasy novel", 12.99m, "The Hobbit" },
                    { 6L, 3L, new DateOnly(1954, 7, 29), "Epic fantasy", 25.99m, "The Lord of the Rings" },
                    { 7L, 4L, new DateOnly(1951, 5, 1), "Science fiction", 10.99m, "Foundation" },
                    { 8L, 4L, new DateOnly(1950, 12, 2), "Robot series", 8.99m, "I, Robot" },
                    { 9L, 5L, new DateOnly(1818, 1, 1), "Gothic horror", 6.99m, "Frankenstein" },
                    { 10L, 6L, new DateOnly(1866, 1, 1), "Psychological novel", 11.99m, "Crime and Punishment" },
                    { 11L, 6L, new DateOnly(1880, 11, 1), "Philosophical novel", 13.99m, "The Brothers Karamazov" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
