using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure;
using DemoBookApp.Infrastructure.Persistence;


namespace DemoBookApp.Application.Handlers
{
    public class AuthorRequestHandler : IAuthorHandler
    {
        private readonly AuthorRepository _repository;

        public AuthorRequestHandler(ApplicationDBContext context)
        {
            _repository = new AuthorRepository(context);
        }

        public async Task<Result> CreateAsync(CreateAuthorRequest request, CancellationToken token)
        {
            try
            {
                await _repository.CreateAsync(request.ToAuthor(), token);
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

        public async Task<ResultOf<List<Author>>> GetAsync(AuthorQuery authorQuery, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetAsync(authorQuery, cancellationToken);
                return ResultOf<List<Author>>.CreateSuccessful(result);
            }
            catch (Exception e)
            {
                return ResultOf<List<Author>>.CreateFailed(e);
            }
        }

        public async Task<ResultOf<Author>> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id, cancellationToken);
                return ResultOf<Author>.CreateSuccessful(result);
            }
            catch (Exception e)
            {
                return ResultOf<Author>.CreateFailed(e);
            }
        }

        public async Task<Result> UpdateAsync(long id, UpdateAuthorRequest request, CancellationToken token)
        {
            try
            {
                await _repository.UpdateAsync(id, request.ToAuthor(), token);
                return Result.CreateSuccessful();
            }
            catch (Exception e)
            {
                return Result.CreateFailed(e);
            }
        }
    }
}