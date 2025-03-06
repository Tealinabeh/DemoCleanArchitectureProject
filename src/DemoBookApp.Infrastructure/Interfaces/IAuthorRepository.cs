using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAsync(AuthorQuery query, CancellationToken token);
        public Task<List<Author>> GetByIdsAsync(IEnumerable<long> id, CancellationToken token);
        public Task<Author> GetByIdAsync(long id, CancellationToken token);
        public Task CreateAsync(Author author, CancellationToken token);
        public Task UpdateAsync(long id, Author updateAuthor, CancellationToken token);
        public Task DeleteAsync(long id, CancellationToken token);
    }
}