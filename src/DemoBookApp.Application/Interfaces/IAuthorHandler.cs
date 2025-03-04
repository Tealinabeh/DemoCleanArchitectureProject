using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Core;

namespace DemoBookApp.Application.Interfaces
{
    public interface IAuthorHandler
    {
        public Task<ResultOf<Author>> GetByIdAsync(long id, CancellationToken token);
        public Task<ResultOf<List<Author>>> GetAsync(AuthorQuery query, CancellationToken token);
        public Task<Result> CreateAsync(CreateAuthorRequest request, CancellationToken token);
        public Task<Result> UpdateAsync(long id, UpdateAuthorRequest request, CancellationToken token);
        public Task<Result> DeleteAsync(long id, CancellationToken token);
    }
}