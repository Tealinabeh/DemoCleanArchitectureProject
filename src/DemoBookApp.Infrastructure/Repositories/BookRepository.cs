using System.Text.Json;
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
            var resultQuery = Books.AsNoTracking().AsQueryable();
            resultQuery = resultQuery.ResolveQuery(query);
            var result = await resultQuery.Include(b => b.Author).ToListAsync(token);

            if (result.Count == 0)
            {
                string jsonQuery = JsonSerializer.Serialize(query, new JsonSerializerOptions { WriteIndented = true });
                throw new ArgumentException($"Couldn't find a single book with given parameters:\n{jsonQuery}");
            }   

            return result;
        }

        public async Task<Book> GetByIdAsync(long id, CancellationToken token)
        {
            var result = await Books.AsNoTracking()
                                .Include(b => b.Author)
                                .FirstOrDefaultAsync(b => b.Id == id, token);

            if (result is null)
            {
                throw new NullDatabaseEntityException($"Book with id {id} doesn't exist.");
            }

            return result;
        }

        public async Task CreateAsync(Book book, CancellationToken token)
        {
            await Books.AddAsync(book, token);
            await _dbContext.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(long id, Book updateBook, CancellationToken token)
        {
            var existingBook = await Books.FirstOrDefaultAsync(b => b.Id == id, token);

            if (existingBook is null)
                throw new NullDatabaseEntityException($"Book with id {id} doesn't exist. Use create method instead.");

            existingBook.UpdateExistingWith(updateBook);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            var book = await Books.FirstOrDefaultAsync(b => b.Id == id, token);

            if (book is null)
                throw new NullReferenceException($"Book with id {id} doesn't exist.");

            Books.Remove(book);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}