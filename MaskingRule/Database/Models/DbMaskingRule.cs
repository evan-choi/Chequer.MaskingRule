using MaskingRule.Data;
using MaskingRule.Database.Cache;
using SQLite;

namespace MaskingRule.Database.Models
{
    [Table("masking_rule")]
    public class DbMaskingRule : INamedCache
    {
        [Column("id")]
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("database_name")]
        public string DatabaseName { get; set; } // scheme name? driver name?

        [Column("table_name")]
        public string TableName { get; set; }

        [Column("column_name")]
        public string ColumnName { get; set; }

        [Column("masking_type")]
        public MaskingType Type
        {
            get => _type;
            set
            {
                _type = value;
                MaskingRule = MaskingRuleFactory.Create(value);
            }
        }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Ignore]
        public IMaskingRule MaskingRule { get; private set; }

        MaskingType _type;

        public override string ToString() => Name;
    }
}
