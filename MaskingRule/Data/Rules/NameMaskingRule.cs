using MaskingRule.Data.Common;
using System.Collections.Generic;
using System.Globalization;

namespace MaskingRule.Data.Rules
{
    class NameMaskingRule : StringMaskingRule
    {
        // 모든문자가 한 언어로만 이뤄져있다고 가정

        protected override IEnumerable<Range> GetMaskRanges(string value)
        {
            if (IsKorean(value))
            {
                return GetKoreanMaskRanges(value);
            }
            else
            {
                return GetEnglighMaskRanges(value);
            }
        }

        private IEnumerable<Range> GetEnglighMaskRanges(string value)
        {
            if (value.Length > 4)
            {
                yield return new Range(4, value.Length - 1);
            }
            else
            {
                yield break;
            }
        }

        private IEnumerable<Range> GetKoreanMaskRanges(string value)
        {
            if (value.Length > 3)
            {
                yield return new Range(1, value.Length - 2);
            }
            else if (value.Length > 1)
            {
                yield return new Range(1, 1);
            }
        }

        private bool IsKorean(string value)
        {
            return char.GetUnicodeCategory(value[0]) == UnicodeCategory.OtherLetter;
        }
    }
}
