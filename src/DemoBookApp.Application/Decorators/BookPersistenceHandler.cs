using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;

namespace DemoBookApp.Application.Decorators
{
    public class BookPersistenceHandler : IBookHandler
    {
        private readonly IBookRepository _repository;
        private readonly IAuthorRepository _authorRepository;

        public BookPersistenceHandler(ApplicationDBContext context, IBookRepository repository, IAuthorRepository authorRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
        }

        public async Task<Result> CreateAsync(CreateBookRequest request, CancellationToken token)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(request.AuthorId, token);
                await _repository.CreateAsync(request.ToBook(author), token);
                return Result.CreateSuccessful();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(e);
            }
        }

        public async Task<Result> DeleteAsync(long id, CancellationToken token)
        {
            try
            {
                await _repository.DeleteAsync(id, token);
                return Result.CreateSuccessful();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(e);
            }
        }

        public async Task<ResultOf<List<Book>>> GetAsync(BookQuery query, CancellationToken token)
        {
            try
            {
                var books = await _repository.GetAsync(query, token);
                return ResultOf<List<Book>>.CreateSuccessful(books);
            }
            catch (Exception e)
            {
                return ResultOf<List<Book>>.CreateFailed(e);
            }
        }

        public async Task<ResultOf<Book>> GetByIdAsync(long id, CancellationToken token)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id, token);
                return ResultOf<Book>.CreateSuccessful(book);
            }
            catch (Exception e)
            {
                return ResultOf<Book>.CreateFailed(e);
            }
        }

        public async Task<Result> UpdateAsync(long id, UpdateBookRequest request, CancellationToken token)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(request.AuthorId, token);
                await _repository.UpdateAsync(id, request.ToBook(author), token);
                return Result.CreateSuccessful();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(e);
            }
        }
    }
}
