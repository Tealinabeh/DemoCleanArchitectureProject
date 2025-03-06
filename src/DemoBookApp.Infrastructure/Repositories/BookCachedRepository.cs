using System.Text;
using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Cache;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace DemoBookApp.Infrastructure.Repositories
{
    public class BookCachedRepository : RedisCacheService, IBookRepository
    {
        private readonly BookRepository _repository;

        public BookCachedRepository(IDistributedCache cache, IConnectionMultiplexer redis, ApplicationDBContext context)
        : base(cache, redis)
        {
            _repository = new BookRepository(context);
        }

        public async Task<List<Book>> GetAsync(BookQuery query, CancellationToken token)
        {
            string cacheKey = ToCacheKey(query);

            var cachedData = await GetCachedValueAsync<List<Book>>(cacheKey, token);
            if(cachedData is not null && cachedData.Count > 0)
                return cachedData;

            var books = await _repository.GetAsync(query, token);

            await StoreInCache(cacheKey, books, token);

            return books;
        }

        public async Task<Book> GetByIdAsync(long id, CancellationToken token)
        {
            string cacheKey = $"book_{id}";

            var cachedData = await GetCachedValueAsync<Book>(cacheKey, token);
            if(cachedData is not null)
                return cachedData;

            var book = await _repository.GetByIdAsync(id, token);
            if (book is not null)
                await StoreInCache(cacheKey, book, token);

            return book;
        }

        public async Task<List<Book>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken token)
        {
            string cacheKey = $"books_{string.Join("_", ids)}";

            var cachedData = await GetCachedValueAsync<List<Book>>(cacheKey, token);
            if(cachedData is not null && cachedData.Count > 0)
                return cachedData;

            var books = await _repository.GetByIdsAsync(ids, token);
            if (books is not null && books.Count > 0)
                await StoreInCache(cacheKey, books, token);

            return books;
        }

        public async Task UpdateAsync(long id, Book updateBook, CancellationToken token)
        {
            await _repository.UpdateAsync(id, updateBook, token);
            await InvalidateCacheAsync($"books_", token);
            await Cache.RemoveAsync($"book_{id}", token);
        }

        public async Task CreateAsync(Book book, CancellationToken token)
        {
            await _repository.CreateAsync(book, token);
            await InvalidateCacheAsync($"books_", token);
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            await _repository.DeleteAsync(id, token);
            await InvalidateCacheAsync($"books_", token);
            await Cache.RemoveAsync($"book_{id}", token);
        }

        private string ToCacheKey(BookQuery query)
        {
            var cacheKeyBuilder = new StringBuilder("books_");

            cacheKeyBuilder.Append(query.Title).Append('_')
                          .Append(query.LowestPrice).Append('_')
                          .Append(query.HighestPrice).Append('_')
                          .Append(query.IssuedAfter).Append('_')
                          .Append(query.IssuedBefore).Append('_')
                          .Append(query.AuthorName).Append('_')
                          .Append(query.AuthorSurname).Append('_')
                          .Append(query.OrderBy).Append('_')
                          .Append(query.IsDescending).Append('_')
                          .Append(query.PageNumber).Append('_')
                          .Append(query.PageSize);

            return cacheKeyBuilder.ToString();
        }
    }
}