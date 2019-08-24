using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class RegnoMaskingRule : StringMaskingRule
    {
        readonly static Regex _regex = new Regex(@"[^\d]?\d{2}[0-1]\d[0-3]\d[-\s][1-6]\d{6}[^\d]?");

        readonly bool _genderVisible;

        public RegnoMaskingRule() : this(false)
        {
        }

        public RegnoMaskingRule(bool genderVisible)
        {
            _genderVisible = genderVisible;
        }

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            if (_genderVisible)
                yield return new Range(8, 13);
            else
                yield return new Range(7, 13);
        }
    }
}
