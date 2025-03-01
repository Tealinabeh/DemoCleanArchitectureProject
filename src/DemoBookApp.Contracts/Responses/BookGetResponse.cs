namespace DemoBookApp.Contracts.Responses
{
    public record BookGetResponse
    (
        long Id,
        string Title,
        string Description,
        decimal Price,
        DateOnly DateOfIssue,
        AuthorDTO Author
    );
}