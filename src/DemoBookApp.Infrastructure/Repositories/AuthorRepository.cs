using System.Text.Json;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Exceptions;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Extensions;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
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

            resultQuery = resultQuery.ResolveQuery(query);
            var result = await resultQuery.Include(a => a.IssuedBooks).ToListAsync(token);

            if (result.Count == 0)
            {
                ThrowQueryException(query);
            }
            return result;
        }

        public async Task<List<Author>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken token)
        {
            var result = await Authors.AsNoTracking()
                                .Include(a => a.IssuedBooks)
                                .Where(a => ids.Contains(a.Id))
                                .ToListAsync(token);
            if (result.Count == 0)
                throw new NullDatabaseEntityException("Couldn't find any authors with given ids.");
    
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
             var affectedRows = 
                await Authors.Where(a => a.Id == id)
                        .ExecuteUpdateAsync(u => u
                        .SetProperty(a => a.Name, updateAuthor.Name)
                        .SetProperty(a => a.Surname, updateAuthor.Surname)
                        .SetProperty(a => a.DateOfBirth, updateAuthor.DateOfBirth), token);

            if(affectedRows == 0)
                throw new NullDatabaseEntityException($"No author found with Id {id}.\nYou may put the wrong Id or the author doesn't exist and you can create one.");
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            var author = await Authors.FirstOrDefaultAsync(a => a.Id == id, token);

            if (author is null)
                throw new NullReferenceException($"Book with id {id} doesn't exist.");

            Authors.Remove(author);
            await _dbContext.SaveChangesAsync(token);
        }
        private static void ThrowQueryException(AuthorQuery query)
        {
            string jsonQuery = JsonSerializer.Serialize(query, new JsonSerializerOptions
            {
                WriteIndented = true, 
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });
            throw new ArgumentException($"Couldn't find a single author with given parameters:\n{jsonQuery}");
        }

    }
}