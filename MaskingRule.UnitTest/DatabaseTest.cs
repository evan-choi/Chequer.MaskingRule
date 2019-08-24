using MaskingRule.Database;
using MaskingRule.Database.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MaskingRule.UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void MaskingRuleDatabase()
        {
            DbMaskingRule[] dbRules = MaskingRuleDB.Instance.GetRules("db-0", "table-0", "name")?.ToArray();
        }
    }
}
