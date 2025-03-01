using DemoBookApp.Core;

namespace DemoBookApp.Contracts
{
    public record BookQuery(
        string? Title,
        decimal? LowestPrice,
        decimal? HighestPrice,
        DateOnly? DateOfIssue,
        Author? Author
    );
}