using System.ComponentModel.DataAnnotations;

namespace DemoBookApp.Contracts
{
    public record BookQuery(
        string? Title,
        [Range(0, 20000)] 
        decimal? LowestPrice,
        [Range(0, 20000)] 
        decimal? HighestPrice,
        DateOnly? IssuedAfter,
        DateOnly? IssuedBefore,
        string? AuthorName,
        string? AuthorSurname,
        string? OrderBy,
        bool IsDescending,
        int PageNumber = 1,
        int PageSize = 20
    );
}