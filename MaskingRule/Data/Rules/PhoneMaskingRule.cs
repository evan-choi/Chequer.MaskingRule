using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class PhoneMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"[^\d]?0(?:70|2|31|32|33|41|42|43|51|52|53|54|55|61|62|63|64)[-\s](\d{3,4})[-\s]\d{4}[^\d]?");

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            Match match = _regex.Match(value);

            if (match.Success)
            {
                var capture = match.Groups[1];
                yield return new Range(capture.Index, capture.Index + capture.Length - 1);
            }
        }

        private Capture GetMaskGroup(Regex regex, string value)
        {
            Match match = regex.Match(value);

            if (match.Success)
                return match.Groups[1];

            return null;
        }
    }
}
