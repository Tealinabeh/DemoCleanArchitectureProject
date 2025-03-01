namespace DemoBookApp.Contracts
{
    public record AuthorDTO
    (
        string Name,
        string Surname,
        DateOnly DateOfBirth
    );
}