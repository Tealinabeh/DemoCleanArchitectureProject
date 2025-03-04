namespace DemoBookApp.Contracts
{
    public record BookDTO
    (
        long Id,
        string Title,
        string Description,
        decimal Price,
        DateOnly DateOfIssue
    );
}