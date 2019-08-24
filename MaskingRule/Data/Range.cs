using System;

namespace MaskingRule.Data
{
    public struct Range
    {
        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public int Length => Maximum - Minimum + 1;

        public Range(int minimum, int maximum)
        {
            if (maximum < minimum)
                throw new ArgumentException($"{nameof(maximum)} < {nameof(minimum)}");

            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
