using MaskingRule.Database;
using MaskingRule.Database.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MaskingRule.Test
{
    class Program
    {
        readonly static Random _random = new Random();
        readonly static string[] _phonePreset = { "70", "2", "31", "32", "33", "41", "42", "43", "51", "52", "53", "54", "55", "61", "62", "63", "64" };
        readonly static string _firstNamePreset = "박이김유장노윤강최우정하민옥";
        readonly static string _lastNamePreset = "리지훈용덕종은남준석영현진섭창유동헌범홍소조";

        static void Main(string[] args)
        {
            var ruleDb = MaskingRuleDB.Instance;

            // 두개의 DB의 Table을 Join하여 결과가 2 Column이라고 가정
            object[][] resultSet = Execute(1000000);
            string[] resultDatabases = { "db-1", "db-2" };
            string[] resultTables = { "table-5", "table-9" };
            string[] resultColumns = { "name", "phone" };

            int columnCount = resultColumns.Length;

            Console.WriteLine("Dummy data created");

            // Data Masking
            var now = DateTime.Now;

            for (int i = 0; i < columnCount; i++)
            {
                DbMaskingRule rule = ruleDb.GetRules(resultDatabases[i], resultTables[i], resultColumns[i])
                    .FirstOrDefault();

                if (rule == null)
                    continue;

                if (resultSet.Length > 10000)
                {
                    Parallel.For(0, resultSet.Length, r =>
                    {
                        resultSet[r][i] = rule.MaskingRule.Execute(resultSet[r][i]);
                    });
                }
                else
                {
                    for (int r = 0; r < resultSet.Length; r++)
                    {
                        resultSet[r][i] = rule.MaskingRule.Execute(resultSet[r][i]);
                    }
                }
            }

            TimeSpan duration = DateTime.Now - now;
            int dataSize = resultSet[0].Length * resultSet.Length;

            Console.WriteLine();
            Console.WriteLine($"DataSize:    {dataSize}");
            Console.WriteLine($"Total:       {duration.TotalMilliseconds}ms");
            Console.WriteLine($"DataMasking: {duration.TotalMilliseconds / dataSize}ms/column");
        }

        static object[][] Execute(int rowCount)
        {
            var resultSet = new object[rowCount][];

            for (int r = 0; r < resultSet.Length; r++)
            {
                resultSet[r] = new object[]
                {
                    CreateRandomName(),
                    CreateRandomPhone()
                };
            }

            return resultSet;
        }

        static string CreateRandomName()
        {
            string result = _firstNamePreset[_random.Next(_firstNamePreset.Length)].ToString();
            int count = _random.Next(1, 5);

            for (int i = 0; i < count; i++)
                result += _lastNamePreset[_random.Next(_lastNamePreset.Length)].ToString();

            return result;
        }

        static string CreateRandomPhone()
        {
            string result = $"0{_phonePreset[_random.Next(_phonePreset.Length)]}-";
            int count = _random.Next(3, 5);

            for (int i = 0; i < count; i++)
                result += _random.Next(10);

            result += '-';

            for (int i = 0; i < 4; i++)
                result += _random.Next(10);

            return result;
        }
    }
}
