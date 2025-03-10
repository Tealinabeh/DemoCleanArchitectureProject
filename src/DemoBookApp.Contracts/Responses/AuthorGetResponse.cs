namespace DemoBookApp.Contracts.Responses
{
    public record AuthorGetResponse
    (
        long Id,
        string Name,
        string Surname,
        DateOnly DateOfBirth,
        ushort age,
        List<BookDTO> IssuedBooks
    );
}