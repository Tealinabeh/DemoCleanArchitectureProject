using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAsync(BookQuery query, CancellationToken token);
        public Task<Book?> GetByIdAsync(long id, CancellationToken token);
  
    }
}