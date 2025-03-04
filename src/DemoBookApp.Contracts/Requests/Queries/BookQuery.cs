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
        [Required, Range(0, 900000)]int PageNumber = 1,
        [Required, Range(0, 900000)]int PageSize = 20
    );
}