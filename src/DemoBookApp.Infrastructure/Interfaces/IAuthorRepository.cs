using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAsync(AuthorQuery query, CancellationToken token);
        public Task<Author?> GetByIdAsync(long id, CancellationToken token);
  
    }
}