namespace DemoBookApp.Contracts
{
    public record BookDTO
    (
        string Title,
        string Description,
        decimal Price,
        DateOnly DateOfIssue
    );
}