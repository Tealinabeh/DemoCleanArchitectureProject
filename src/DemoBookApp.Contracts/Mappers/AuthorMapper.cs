using DemoBookApp.Core;

namespace DemoBookApp.Contracts.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorGetResponse ToGetResponse(this Author author)
        {
            return new AuthorGetResponse
            (
                author.Id,
                author.Name,
                author.Surname,
                author.DateOfBirth,
                author.CalculateAge(),
                author.IssuedBooks
                        .Select(b => b.ToDTO())
                        .ToList()
            );
        }

        public static AuthorDTO ToDTO(this Author author){
            return new AuthorDTO
            (
                author.Name,
                author.Surname,
                author.DateOfBirth
            );
        } 

        public static ushort CalculateAge(this Author author)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            var age = today.Year - author.DateOfBirth.Year;

            if (today < new DateOnly(today.Year, author.DateOfBirth.Month, author.DateOfBirth.Day))
                age--;

            return (ushort)age;
        }
    }
}