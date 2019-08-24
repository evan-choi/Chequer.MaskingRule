using MaskingRule.Database.Models;

namespace MaskingRule.Database.Cache
{
    public sealed class DbColumnCache : NamedCache<DbMaskingRule>
    {
        public DbColumnCache(string name) : base(name)
        {
        }
    }
}
