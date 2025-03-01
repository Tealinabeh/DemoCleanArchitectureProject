using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetAsync(AuthorQuery authorQuery, CancellationToken cancellationToken);
        public Task<Author?> GetByIdAsync(long id, CancellationToken cancellationToken);
  
    }
}