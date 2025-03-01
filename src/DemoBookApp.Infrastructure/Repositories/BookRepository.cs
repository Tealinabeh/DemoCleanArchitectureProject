using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Extensions;
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

        public async Task<List<Book>> GetAsync(BookQuery query, CancellationToken token)
        {
            var result = Books.AsQueryable();

            result.ResolveQuery(query);

            return await result.ToListAsync(token);
        }

        public async Task<Book?> GetByIdAsync(long id, CancellationToken token)
        {
            return await Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, token);
        }
    }
}