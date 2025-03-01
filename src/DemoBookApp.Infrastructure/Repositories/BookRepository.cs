using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private DbSet<Book> Books => _dbContext.Books;

        public BookRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        
        public Task<List<Book>> GetAsync(BookQuery query, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetByIdAsync(long id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}