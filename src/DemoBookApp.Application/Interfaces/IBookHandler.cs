using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Core;

namespace DemoBookApp.Application.Interfaces
{
    public interface IBookHandler
    {
        public Task<ResultOf<Book>> GetByIdAsync(long id, CancellationToken token);
        public Task<ResultOf<List<Book>>> GetAsync(BookQuery query, CancellationToken token);
        public Task<Result> CreateAsync(CreateBookRequest request, CancellationToken token);
        public Task<Result> UpdateAsync(long id, UpdateBookRequest request, CancellationToken token);
        public Task<Result> DeleteAsync(long id, CancellationToken token);
    }
}