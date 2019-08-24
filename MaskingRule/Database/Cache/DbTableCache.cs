namespace MaskingRule.Database.Cache
{
    public sealed class DbTableCache : NamedCache<DbColumnCache>
    {
        public DbTableCache(string name) : base(name)
        {
        }
    }
}
