using System.Text;
using DemoBookApp.Contracts;
using DemoBookApp.Core;
using DemoBookApp.Infrastructure.Interfaces;
using DemoBookApp.Infrastructure.Cache;
using DemoBookApp.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace DemoBookApp.Infrastructure.Repositories
{
    public class AuthorCachedRepository : RedisCacheService, IAuthorRepository
    {
        private readonly AuthorRepository _repository;

        public AuthorCachedRepository(IDistributedCache cache, IConnectionMultiplexer redis, ApplicationDBContext context)
        : base(cache, redis)
        {
            _repository = new AuthorRepository(context);
        }

        public async Task<List<Author>> GetAsync(AuthorQuery query, CancellationToken token)
        {
            string cacheKey = ToCacheKey(query);

            var cachedData = await GetCachedValueAsync<List<Author>>(cacheKey, token);
            if(cachedData is not null && cachedData.Count > 0)
                return cachedData;

            var authors = await _repository.GetAsync(query, token);

            await StoreInCache(cacheKey, authors, token);

            return authors;
        }

        public async Task<Author> GetByIdAsync(long id, CancellationToken token)
        {
            string cacheKey = $"author_{id}";

            var cachedData = await GetCachedValueAsync<Author>(cacheKey, token);
            if(cachedData is not null)
                return cachedData;

            var author = await _repository.GetByIdAsync(id, token);
            if (author is not null)
                await StoreInCache(cacheKey, author, token);

            return author;
        }

        public async Task<List<Author>> GetByIdsAsync(IEnumerable<long> ids, CancellationToken token)
        {
            string cacheKey = $"authors_{string.Join("_", ids)}";

            var cachedData = await GetCachedValueAsync<List<Author>>(cacheKey, token);
            if(cachedData is not null && cachedData.Count > 0)
                return cachedData;

            var authors = await _repository.GetByIdsAsync(ids, token);
            if (authors is not null && authors.Count > 0)
                await StoreInCache(cacheKey, authors, token);

            return authors;
        }

        public async Task CreateAsync(Author author, CancellationToken token)
        {
            await _repository.CreateAsync(author, token);
            await InvalidateCacheAsync($"authors_", token);
        }

        public async Task DeleteAsync(long id, CancellationToken token)
        {
            await _repository.DeleteAsync(id, token);
            await InvalidateCacheAsync($"author_{id}", token);
            await Cache.RemoveAsync($"author_{id}", token);
        }

        public async Task UpdateAsync(long id, Author updateAuthor, CancellationToken token)
        {
            await _repository.UpdateAsync(id, updateAuthor, token);
            await InvalidateCacheAsync($"author_{id}", token);
            await Cache.RemoveAsync($"author_{id}", token);
        }
        private string ToCacheKey(AuthorQuery query)
        {
            var cacheKeyBuilder = new StringBuilder("authors_");

            cacheKeyBuilder.Append(query.Name).Append('_')
                          .Append(query.Surname).Append('_')
                          .Append(query.OrderBy).Append('_')
                          .Append(query.IsDescending).Append('_')
                          .Append(query.PageNumber).Append('_')
                          .Append(query.PageSize);

            return cacheKeyBuilder.ToString();
        }
    }
}