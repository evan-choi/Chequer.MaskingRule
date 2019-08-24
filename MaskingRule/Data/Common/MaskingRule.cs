using System;

namespace MaskingRule.Data.Common
{
    public abstract class MaskingRule<T> : IMaskingRule
    {
        protected abstract T Execute(T value);

        object IMaskingRule.Execute(object value)
        {
            if (value is T tValue)
                return Execute(tValue);

            throw new NotSupportedException();
        }
    }
}
