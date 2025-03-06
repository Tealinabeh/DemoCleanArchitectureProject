using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAsync(BookQuery query, CancellationToken token);
        public Task<List<Book>> GetByIdsAsync(IEnumerable<long> id, CancellationToken token);
        public Task<Book> GetByIdAsync(long id, CancellationToken token);
        public Task CreateAsync(Book book, CancellationToken token);
        public Task UpdateAsync(long id, Book updateBook, CancellationToken token);
        public Task DeleteAsync(long id, CancellationToken token);
    }
}