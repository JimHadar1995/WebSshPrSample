using Microsoft.Extensions.Caching.Memory;

namespace Library.Common.Types.Wrappers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICacheWrapper" />
    public class MemoryCacheWrapper : ICacheWrapper
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryCacheWrapper"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        ///<inheritdoc />
        public object this[string key]
        {
            get => Get(key);
            set => Set(key, value);
        }

        ///<inheritdoc />
        public bool IsInitialized => true;

        ///<inheritdoc />
        public int Count => ((MemoryCache)_memoryCache).Count;

        ///<inheritdoc />
        public object Get(string key) =>
            _memoryCache.Get(key);

        ///<inheritdoc />
        public void Remove(string key) =>
            _memoryCache.Remove(key);

        ///<inheritdoc />
        public void Set(string key, object value) =>
            _memoryCache.Set(key, value);

        ///<inheritdoc />
        public void Set<T>(string key, T value) =>
            _memoryCache.Set<T>(key, value);

        ///<inheritdoc />
        public bool TryGetValue(string key, out object value) =>
            _memoryCache.TryGetValue(key, out value);

        ///<inheritdoc />
        public bool TryGetValue<T>(string key, out T value) =>
            _memoryCache.TryGetValue<T>(key, out value);
    }
}
