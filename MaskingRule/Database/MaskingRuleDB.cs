using MaskingRule.Data;
using MaskingRule.Database.Cache;
using MaskingRule.Database.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaskingRule.Database
{
    public sealed class MaskingRuleDB
    {
        readonly SQLiteConnection _connection;
        Dictionary<string, DbCache> _dbCache;
        
        private MaskingRuleDB()
        {
            _connection = new SQLiteConnection("rules.db");
            _connection.CreateTable<DbMaskingRule>();
            _dbCache = new Dictionary<string, DbCache>();

            CreateDummyRules();

            LoadRules();
        }

        void CreateDummyRules()
        {
            var rules = new List<DbMaskingRule>();

            // database
            for (int i = 0; i < 5; i++)
            {
                string databaseName = $"db-{i}";

                // table
                for (int j = 0; j < 10; j++)
                {
                    string tableName = $"table-{j}";

                    // masking type
                    foreach (MaskingType type in Enum.GetValues(typeof(MaskingType)))
                    {
                        rules.Add(new DbMaskingRule()
                        {
                            Name = $"rule_{i}_{j}_{type.ToString()}",
                            DatabaseName = databaseName,
                            TableName = tableName,
                            ColumnName = type.ToString().ToLower(),
                            Type = type,
                            IsUsed = true
                        });
                    }
                }
            }

            _connection.BeginTransaction();
            _connection.DeleteAll<DbMaskingRule>();
            _connection.InsertAll(rules);
            _connection.Commit();
        }

        void LoadRules()
        {
            _dbCache = BuildDbCaches(_connection.Table<DbMaskingRule>()).ToDictionary(db => db.Name);

            IEnumerable<DbCache> BuildDbCaches(IEnumerable<DbMaskingRule> rules)
            {
                return rules
                    .GroupBy(r => r.DatabaseName)
                    .Select(dbGroup =>
                    {
                        var db = new DbCache(dbGroup.Key);
                        db.AddRange(BuildTableCaches(dbGroup));
                        return db;
                    });
            }

            IEnumerable<DbTableCache> BuildTableCaches(IGrouping<string, DbMaskingRule> group)
            {
                return group
                    .GroupBy(r => r.TableName)
                    .Select(tableGroup =>
                    {
                        var table = new DbTableCache(tableGroup.Key);
                        table.AddRange(BuildColumnCaches(tableGroup));
                        return table;
                    });
            }

            IEnumerable<DbColumnCache> BuildColumnCaches(IGrouping<string, DbMaskingRule> group)
            {
                return group
                    .GroupBy(r => r.ColumnName)
                    .Select(columnGroup =>
                    {
                        var column = new DbColumnCache(columnGroup.Key);
                        column.AddRange(columnGroup);
                        return column;
                    });
            }
        }

        public IEnumerable<DbCache> GetDatabaseCaches()
        {
            return _dbCache.Values;
        }

        public IEnumerable<DbTableCache> GetTableCaches(string database)
        {
            if (!_dbCache.TryGetValue(database, out DbCache dbCache))
                return Enumerable.Empty<DbTableCache>();

            return dbCache;
        }

        public IEnumerable<DbColumnCache> GetColumnCaches(string database, string table)
        {
            if (!_dbCache.TryGetValue(database, out DbCache dbCache))
                return Enumerable.Empty<DbColumnCache>();

            if (!dbCache.TryGetValue(table, out DbTableCache tableCache))
                return Enumerable.Empty<DbColumnCache>();

            return tableCache;
        }

        public IEnumerable<DbMaskingRule> GetRules(string database, string table, string column)
        {
            if (!_dbCache.TryGetValue(database, out DbCache dbCache))
                return Enumerable.Empty<DbMaskingRule>();

            if (!dbCache.TryGetValue(table, out DbTableCache tableCache))
                return Enumerable.Empty<DbMaskingRule>();

            if (!tableCache.TryGetValue(column, out DbColumnCache columnCache))
                return Enumerable.Empty<DbMaskingRule>();

            return columnCache;
        }

        #region Singleton
        public static MaskingRuleDB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MaskingRuleDB();

                return _instance;
            }
        }

        static MaskingRuleDB _instance;
        #endregion
    }
}
