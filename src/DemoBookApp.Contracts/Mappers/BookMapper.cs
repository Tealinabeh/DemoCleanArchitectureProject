using DemoBookApp.Contracts.Responses;
using DemoBookApp.Core;

namespace DemoBookApp.Contracts.Mappers
{
    public static class BookMapper
    {
        public static BookGetResponse ToGetResponse(this Book book)
        {
            return new BookGetResponse
            (
                book.Id,
                book.Title,
                book.Description,
                book.Price,
                book.DateOfIssue,
                book.Author.ToDTO()
            );
        }
        
         public static BookDTO ToDTO(this Book book)
         {
            return new BookDTO
            (
                book.Title,
                book.Description,
                book.Price,
                book.DateOfIssue
            );
        } 
    }
}