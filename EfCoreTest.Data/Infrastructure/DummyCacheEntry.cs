using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace EfCoreTest.Data.Infrastructure
{
    public class DummyCacheEntry : ICacheEntry
    {
        public void Dispose()
        {
            
        }

        public object Key { get; } = null;
        public object Value { get; set; } = null;
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
        public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();
        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; } = new List<PostEvictionCallbackRegistration>();
        public CacheItemPriority Priority { get; set; } = CacheItemPriority.Low;
        public long? Size { get; set; } = 0;
    }
}