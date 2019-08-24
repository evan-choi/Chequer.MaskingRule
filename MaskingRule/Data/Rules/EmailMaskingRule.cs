using MaskingRule.Data.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MaskingRule.Data.Rules
{
    class EmailMaskingRule : StringMaskingRule
    {
        readonly Regex _regex = new Regex(@"[^0-9]?[a-zA-Z0-9._%+-]+@[a-zA-Z09.-]+\.[a-zA-Z]{2,4}[^0-9]");

        readonly bool _visibleDomain;

        public EmailMaskingRule() : this(false)
        {
        }

        public EmailMaskingRule(bool visibleDomain)
        {
            _visibleDomain = visibleDomain;
        }

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (!_regex.IsMatch(value))
                yield break;

            int atIndex = value.IndexOf('@');

            if (_visibleDomain)
            {
                int length = (int)Math.Ceiling(atIndex / 2f);
                int startIndex = (atIndex - length) / 2;

                yield return new Range(startIndex, startIndex + length - 1);
            }
            else
            {
                int dotIndex = value.IndexOf('.', atIndex);

                yield return new Range(0, atIndex - 1);
                yield return new Range(atIndex + 1, dotIndex - 1);
            }
        }
    }
}
