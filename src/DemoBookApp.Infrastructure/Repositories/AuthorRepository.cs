using System.Text.Json;
using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Extensions;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DemoBookApp.Infrastructure
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private DbSet<Author> Authors => _dbContext.Authors;

        public AuthorRepository(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        public async Task<List<Author>> GetAsync(AuthorQuery query, CancellationToken token)
        {
            var resultQuery = Authors.AsQueryable();

            resultQuery.ResolveQuery(query);
            var result = await resultQuery.Include(a => a.IssuedBooks).ToListAsync(token);

            if (result.Count == 0)
            {
                string jsonQuery = JsonSerializer.Serialize(query, new JsonSerializerOptions { WriteIndented = true });
                throw new ArgumentException($"Couldn't find a single author with given parameters:\n{jsonQuery}");
            }
            return result;
        }

        public async Task<Author> GetByIdAsync(long id, CancellationToken token)
        {
            var result = await Authors.AsNoTracking()
                                .Include(a => a.IssuedBooks)
                                .FirstOrDefaultAsync(a => a.Id == id, token);
            if (result is null)
                throw new NullDatabaseEntityException($"There is no such author with id {id}");
    
            return result;
        }

        public async Task CreateAsync(Author author, CancellationToken token)
        {
            await Authors.AddAsync(author, token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(long id, Author updateAuthor, CancellationToken token)
        {
            var existingAuthor = await Authors.FirstOrDefaultAsync(a => a.Id == id, token);

            if (existingAuthor is null)
                throw new NullDatabaseEntityException($"Author with id {id} doesn't exist. Use create method instead.");

            existingAuthor.UpdateWithExisting(updateAuthor);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            var author = await Authors.FirstOrDefaultAsync(a => a.Id == id, token);

            if (author is null)
                throw new NullReferenceException($"Book with id {id} doesn't exist.");

            Authors.Remove(author);
            await _dbContext.SaveChangesAsync(token);
        }
    }
}