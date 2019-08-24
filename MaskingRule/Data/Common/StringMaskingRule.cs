using System;
using System.Collections.Generic;

namespace MaskingRule.Data.Common
{
    public abstract class StringMaskingRule : MaskingRule<string>
    {
        public char MaskingCharacter { get; set; } = '*';

        protected override string Execute(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            char[] buffer = value.ToCharArray();

            foreach (Range range in GetMaskRanges(value))
            {
                if (range.Minimum < 0 || range.Maximum > buffer.Length)
                    throw new IndexOutOfRangeException();

                for (int i = range.Minimum; i <= range.Maximum; i++)
                    buffer[i] = MaskingCharacter;
            }

            return new string(buffer);
        }

        protected abstract IEnumerable<Range> GetMaskRanges(string value);
    }
}
