using Microsoft.Extensions.Caching.Memory;

namespace EfCoreTest.Data.Infrastructure
{
    public class DummyMemoryCache : IMemoryCache
    {
        public void Dispose()
        {
            
        }

        public bool TryGetValue(object key, out object value)
        {
            value = null;
            return false;
        }

        public ICacheEntry CreateEntry(object key)
        {
            return new DummyCacheEntry();
        }

        public void Remove(object key)
        {
            
        }
    }
}