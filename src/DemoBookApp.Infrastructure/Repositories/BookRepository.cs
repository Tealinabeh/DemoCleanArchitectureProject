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
                ThrowQueryException(query);
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
            var affectedRows = 
                await Books.Where(b => b.Id == id)
                        .ExecuteUpdateAsync(u => u
                        .SetProperty(b => b.Title, updateBook.Title)
                        .SetProperty(b => b.Description, updateBook.Description)
                        .SetProperty(b => b.DateOfIssue, updateBook.DateOfIssue)
                        .SetProperty(b => b.Price, updateBook.Price)
                        .SetProperty(b => b.AuthorId, updateBook.AuthorId), token);

            if(affectedRows == 0)
                throw new NullDatabaseEntityException($"No book found with Id {id}.\nYou may put the wrong Id or the book doesn't exist and you can create it.");
            
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            var affectedRows = await Books.Where(b => b.Id == id).ExecuteDeleteAsync(token);

            if(affectedRows == 0)
                throw new NullDatabaseEntityException($"No book found with Id {id} while deletion.");
        }

        private static void ThrowQueryException(BookQuery query)
        {
            string jsonQuery = JsonSerializer.Serialize(query, new JsonSerializerOptions
            {
                WriteIndented = true, 
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
            throw new ArgumentException($"Couldn't find a single book with given parameters:\n{jsonQuery}");
        }
    }
}