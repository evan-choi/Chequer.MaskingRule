namespace MaskingRule.Database.Cache
{
    public sealed class DbCache : NamedCache<DbTableCache>
    {
        public DbCache(string name) : base(name)
        {
        }
    }
}
