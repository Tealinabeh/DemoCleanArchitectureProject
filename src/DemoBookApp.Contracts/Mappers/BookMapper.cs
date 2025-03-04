using DemoBookApp.Contracts.Requests;
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
        public static Book ToBook(this CreateBookRequest request)
        {
            return new Book{
                Id = 0,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                DateOfIssue = request.DateOfIssue,
                AuthorId = request.AuthorId,
            };
        }
        public static Book ToBook(this UpdateBookRequest request)
        {
            return new Book{
                Id = 0,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                DateOfIssue = request.DateOfIssue,
                AuthorId = request.AuthorId,
            };
        }
        
        public static BookDTO ToDTO(this Book book)
        {
            return new BookDTO
            (
                book.Id,
                book.Title,
                book.Description,
                book.Price,
                book.DateOfIssue
            );
        }
    }
}