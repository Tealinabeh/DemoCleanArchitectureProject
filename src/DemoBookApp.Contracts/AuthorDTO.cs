namespace DemoBookApp.Contracts
{
    public record AuthorDTO
    (
        long Id,
        string Name,
        string Surname,
        DateOnly DateOfBirth
    );
}