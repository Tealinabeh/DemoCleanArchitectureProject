using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAsync(BookQuery query, CancellationToken token);
        public Task<Book?> GetByIdAsync(long id, CancellationToken token);
        public Task<bool> CreateAsync(Book book, CancellationToken token);
        public Task<bool> UpdateAsync(BookQuery query, CancellationToken token);
        public Task<bool> DeleteAsync(Book book, CancellationToken token);
    }
}