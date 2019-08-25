using System.Collections.Generic;

namespace MaskingRule.Database.Cache
{
    public sealed class DbTableCache : NamedCache<DbColumnCache>
    {
        public DbTableCache(string name, IEnumerable<DbColumnCache> items) : base(name, items)
        {
        }
    }
}
