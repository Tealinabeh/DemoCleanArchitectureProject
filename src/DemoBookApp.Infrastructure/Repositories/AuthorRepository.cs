using DemoBookApp.Contracts;
using DemoBookApp.Core;
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

        public async Task<List<Author>> GetAsync(AuthorQuery authorQuery, CancellationToken cancellationToken)
        {
            var result = Authors.AsNoTracking();

            if(!string.IsNullOrEmpty(authorQuery.Name)) 
                result.Where(a => a.Name == authorQuery.Name);

            if(!string.IsNullOrEmpty(authorQuery.Surname)) 
                result.Where(a => a.Name == authorQuery.Surname); 

                return await result.ToListAsync(cancellationToken);
        }

        public async Task<Author?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await Authors.AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}