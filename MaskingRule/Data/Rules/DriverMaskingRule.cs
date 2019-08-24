using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class DriverMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"[^0-9]?[1-2][0-9][-\s]?\d{2}[-\s]\d{6}[-\s ]\d{2}[^0-9]?");

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            yield return new Range(5, 10);
            yield return new Range(12, 13);
        }
    }
}
