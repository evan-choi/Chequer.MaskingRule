using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class PassportMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"[^A-Za-z0-9]?[MSRODmsrod]\d{8}[^0-9@]?");

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            yield return new Range(1, 8);
        }
    }
}
