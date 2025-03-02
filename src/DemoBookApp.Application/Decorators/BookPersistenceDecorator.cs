using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using DemoBookApp.Infrastructure.Repositories;

namespace DemoBookApp.Application.Decorators
{
    public class BookPersistenceDecorator : IBookRepository
    {
         private readonly BookRepository _repository;
        
        public BookPersistenceDecorator(ApplicationDBContext context)
        {
            _repository = new BookRepository(context);
        }

        public Task<List<Book>> GetAsync(BookQuery query, CancellationToken token)
        {
            return _repository.GetAsync(query, token);
        }
 
        public Task<Book?> GetByIdAsync(long id, CancellationToken token)
        {
            return _repository.GetByIdAsync(id, token);
        }
    }
}