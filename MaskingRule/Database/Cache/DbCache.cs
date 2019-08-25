using System.Collections.Generic;

namespace MaskingRule.Database.Cache
{
    public sealed class DbCache : NamedCache<DbTableCache>
    {
        public DbCache(string name, IEnumerable<DbTableCache> items) : base(name, items)
        {
        }
    }
}
