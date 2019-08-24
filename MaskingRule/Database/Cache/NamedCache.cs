using System.Collections;
using System.Collections.Generic;

namespace MaskingRule.Database.Cache
{
    public abstract class NamedCache<T> : INamedCache, IEnumerable<T>
        where T : INamedCache
    {
        public string Name { get; }

        readonly IDictionary<string, T> _cache;

        public NamedCache(string name)
        {
            Name = name;
            _cache = new Dictionary<string, T>();
        }

        public void Add(T item)
        {
            _cache[item.Name] = item;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                _cache[item.Name] = item;
        }

        public void Remove(T item)
        {
            _cache.Remove(item.Name);
        }

        public bool TryGetValue(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public IEnumerator<T> GetEnumerator() => _cache.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
