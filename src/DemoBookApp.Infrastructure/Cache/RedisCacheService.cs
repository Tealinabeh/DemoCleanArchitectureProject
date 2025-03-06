using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DemoBookApp.Infrastructure.Cache
{
    public abstract class RedisCacheService
    {
        protected readonly IDistributedCache Cache;
        protected readonly IConnectionMultiplexer Redis;
        public RedisCacheService(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            Redis = redis;
            Cache = cache;
        }

        protected async Task<T> GetCachedValueAsync<T>(string cacheKey, CancellationToken token)
        {
            var cachedData = await Cache.GetStringAsync(cacheKey, token);
            if (!string.IsNullOrWhiteSpace(cachedData))
            {
                return JsonConvert.DeserializeObject<T>(cachedData, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                });
            }
            return default;
        }
        protected async Task StoreInCache(string cacheKey, object cacheEntity, CancellationToken token, int expireInMinutes = 5, int refreshInMinutes = 2)
        {
            var serializedEntity = JsonConvert.SerializeObject(cacheEntity, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                Formatting = Formatting.Indented
            });

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expireInMinutes),
                SlidingExpiration = TimeSpan.FromMinutes(refreshInMinutes)
            };

            await Cache.SetStringAsync(cacheKey, serializedEntity, cacheOptions, token);
        }
        protected async Task InvalidateCacheAsync(string keyPrefix, CancellationToken token)
        {
            var server = Redis.GetServer(Redis.GetEndPoints().First());
            foreach (var key in server.Keys(pattern: $"{keyPrefix}*"))
            {
                await Cache.RemoveAsync(key, token);
            }
        }
    }
}