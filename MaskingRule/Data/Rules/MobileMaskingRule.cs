using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class MobileMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"[^\d]?(?:01[016789])[-\s]\d{3,4}[-\s]\d{4}[^\d]?");
        
        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            if (value.Length == 13)
                yield return new Range(4, 7);
            else
                yield return new Range(4, 6);
        }
    }
}
