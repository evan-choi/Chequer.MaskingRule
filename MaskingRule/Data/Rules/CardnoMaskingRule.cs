using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class CardnoMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"\d{4}[-\s]?\d{4}[-\s]\d{4}[-\s]\d{4}");

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            yield return new Range(7, 8);
            yield return new Range(10, 13);
        }
    }
}
