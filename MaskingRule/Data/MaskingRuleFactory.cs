using MaskingRule.Data.Rules;
using System;
using System.Collections.Generic;

namespace MaskingRule.Data
{
    public static class MaskingRuleFactory
    {
        static readonly Dictionary<MaskingType, IMaskingRule> _cache =
            new Dictionary<MaskingType, IMaskingRule>();

        public static IMaskingRule Create(MaskingType type)
        {
            if (!_cache.TryGetValue(type, out IMaskingRule rule))
                _cache[type] = rule = CreateImpl(type);

            return rule;
        }

        static IMaskingRule CreateImpl(MaskingType type)
        {
            switch (type)
            {
                case MaskingType.Name:
                    return new NameMaskingRule();

                case MaskingType.Regno:
                    return new RegnoMaskingRule();

                case MaskingType.RegnoGender:
                    return new RegnoMaskingRule(true);

                case MaskingType.Mobile:
                    return new MobileMaskingRule();

                case MaskingType.Phone:
                    return new PhoneMaskingRule();

                case MaskingType.Email:
                    return new EmailMaskingRule();

                case MaskingType.EmailDomain:
                    return new EmailMaskingRule(true);

                case MaskingType.Cardno:
                    return new CardnoMaskingRule();

                case MaskingType.Driver:
                    return new DriverMaskingRule();

                case MaskingType.Passport:
                    return new PassportMaskingRule();
            }

            throw new NotSupportedException();
        }
    }
}
