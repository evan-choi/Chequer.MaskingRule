using MaskingRule.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MaskingRule.UnitTest
{
    [TestClass]
    public class MaskingRuleTest
    {
        [TestMethod]
        public void Name()
        {
            IMaskingRule rule = MaskingRuleFactory.Create(MaskingType.Name);

            Test(rule, "최진용", "최*용");
            Test(rule, "진용", "진*");
            Test(rule, "이름네자", "이**자");
            Test(rule, "네글자이상", "네***상");

            Test(rule, "ChoiJinYong", "Choi*******");
        }

        [TestMethod]
        public void Regno()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Regno);

            // 정책 1

            Test(rule, "850101-1234567", "850101-*******");
            Test(rule, "850101 2345678", "850101 *******");

            // 정책 2

            rule = MaskingRuleFactory.Create(MaskingType.RegnoGender);

            Test(rule, "850101-1234567", "850101-1******");
            Test(rule, "850101 2345678", "850101 2******");
        }

        [TestMethod]
        public void Mobile()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Mobile);

            Test(rule, "010-1234-5678", "010-****-5678");
            Test(rule, "019 123 4567", "019 *** 4567");
        }

        [TestMethod]
        public void Phone()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Phone);

            Test(rule, "070-1234-5678", "070-****-5678");
            Test(rule, "031 123 4567", "031 *** 4567");

            Test(rule, "02-1234-5678", "02-****-5678");
            Test(rule, "02 123 4567", "02 *** 4567");
        }

        [TestMethod]
        public void Email()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Email);

            // 정책 1

            Test(rule, "godhoop@gmail.com", "*******@*****.com");
            Test(rule, "develope_e@naver.com", "**********@*****.com");

            // 정책 2

            rule = MaskingRuleFactory.Create(MaskingType.EmailDomain);

            Test(rule, "godhoop@gmail.com", "g****op@gmail.com");
            Test(rule, "develope_e@naver.com", "de*****e_e@naver.com");
        }

        [TestMethod]
        public void Cardno()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Cardno);

            Test(rule, "0123-4567-8912-3456", "0123-45**-****-3456");
            Test(rule, "0123 4567 8912 3456", "0123 45** **** 3456");
        }

        [TestMethod]
        public void Driver()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Driver);

            Test(rule, "1234-567890-12", "1234-******-**");
            Test(rule, "2345 678901 23", "2345 ****** **");
        }

        [TestMethod]
        public void Passport()
        {
            var rule = MaskingRuleFactory.Create(MaskingType.Passport);

            Test(rule, "M12345678", "M********");
            Test(rule, "S54096123A", "S********A");
        }

        void Test(IMaskingRule rule, string value, string match)
        {
            var result = (string)rule.Execute(value);

            if (match != result)
                throw new Exception($"Input: {value}, Result: {result}, Match: {match}");
        }
    }
}
