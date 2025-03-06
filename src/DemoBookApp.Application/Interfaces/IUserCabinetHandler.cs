using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Responses;

namespace DemoBookApp.Application.Interfaces
{
    public interface IUserHandler
    {
        public Task<ResultOf<List<AuthorGetResponse>>> GetFavoriteAuthorsAsync(Guid userId, CancellationToken token);
        public Task<ResultOf<List<BookGetResponse>>> GetFavoriteBooksAsync(Guid userId, CancellationToken token);
        public Task<Result> AddAuthorToFavoritesAsync(Guid userId, long authorId, CancellationToken token);
        public Task<Result> AddBookToFavoritesAsync(Guid userId, long bookId, CancellationToken token);
        public Task<Result> RemoveAuthorFromFavoritesAsync(Guid userId, long authorId, CancellationToken token);
        public Task<Result> RemoveBookFromFavoritesAsync(Guid userId, long bookId, CancellationToken token);
    }
}