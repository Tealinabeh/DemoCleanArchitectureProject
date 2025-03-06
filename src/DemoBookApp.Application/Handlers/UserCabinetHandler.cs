using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Exceptions;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Responses;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DemoBookApp.Application.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorRepository _authorRepo;
        private readonly IBookRepository _bookRepo;

        public UserHandler(UserManager<ApplicationUser> userManager, IAuthorRepository authorRepo, IBookRepository repository)
        {
            _bookRepo = repository;
            _authorRepo = authorRepo;
            _userManager = userManager;
        }

        public async Task<Result> AddAuthorToFavoritesAsync(Guid userId, long authorId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            if(user.FavoriteAuthors.Contains(authorId))
                return Result.CreateFailed(new NullReferenceException($"The author is already in favorites."));
            try
            {
                await _authorRepo.GetByIdAsync(authorId, token);
            }catch(NullDatabaseEntityException e)
            {
                return Result.CreateFailed(e);
            }
            user.FavoriteAuthors.Add(authorId);

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
                return Result.CreateFailed(new Exception($"{result.Errors}"));

            return Result.CreateSuccessful();
        }

        public async Task<Result> AddBookToFavoritesAsync(Guid userId, long bookId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            if(user.FavoriteBooks.Contains(bookId))
                return Result.CreateFailed(new NullReferenceException("The book is already in favorites."));
            try
            {
                await _bookRepo.GetByIdAsync(bookId, token);
            }
            catch(NullDatabaseEntityException e)
            {
                return Result.CreateFailed(e);
            }

            user.FavoriteBooks.Add(bookId);

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
                return Result.CreateFailed(new Exception($"{result.Errors}"));

            return Result.CreateSuccessful();
        }

        public async Task<ResultOf<List<AuthorGetResponse>>> GetFavoriteAuthorsAsync(Guid userId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return ResultOf<List<AuthorGetResponse>>
                        .CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            var authors = await _authorRepo.GetByIdsAsync(user.FavoriteAuthors, token);

            return ResultOf<List<AuthorGetResponse>>
                    .CreateSuccessful(authors.Select(a => a.ToGetResponse()).ToList());
        }

        public async Task<ResultOf<List<BookGetResponse>>> GetFavoriteBooksAsync(Guid userId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return ResultOf<List<BookGetResponse>>
                        .CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            var books = await _bookRepo.GetByIdsAsync(user.FavoriteBooks, token);

            return ResultOf<List<BookGetResponse>>
                    .CreateSuccessful(books.Select(b => b.ToGetResponse()).ToList());
        }

        public async Task<Result> RemoveAuthorFromFavoritesAsync(Guid userId, long authorId, CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            var removedSuccessfully = user.FavoriteAuthors.Remove(authorId);

            if (!removedSuccessfully)
                return Result.CreateFailed(new NullReferenceException("The author is not in favorites."));

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
                return Result.CreateFailed(new Exception($"{result.Errors}"));

            return Result.CreateSuccessful();
        }

        public async Task<Result> RemoveBookFromFavoritesAsync(Guid userId, long bookId, CancellationToken token)
        {
             var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return Result.CreateFailed(new UnauthorizedAccessException($"Couldn't find user with Id:\n{userId}."));

            var removedSuccessfully = user.FavoriteBooks.Remove(bookId);

            if (!removedSuccessfully)
                return Result.CreateFailed(new NullReferenceException("The book is not in favorites."));

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
                return Result.CreateFailed(new Exception($"{result.Errors}"));

            return Result.CreateSuccessful();
        }
    }

}