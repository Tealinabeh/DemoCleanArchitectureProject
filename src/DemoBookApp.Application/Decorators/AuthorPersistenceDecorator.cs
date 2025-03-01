using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;


namespace DemoBookApp.Application.Decorators
{
    public class AuthorPersistenceDecorator : IAuthorRepository
    {
        private readonly AuthorRepository _repository;
        
        public AuthorPersistenceDecorator(ApplicationDBContext context)
        {
            _repository = new AuthorRepository(context);
        }

        public async Task<List<Author>> GetAsync(AuthorQuery authorQuery, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(authorQuery, cancellationToken);
        }

        public async Task<Author?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }
    }
}