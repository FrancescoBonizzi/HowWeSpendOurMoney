using System;

namespace HowWeSpendOurMoney.Exceptions
{
    public class DuplicatedRuleException : Exception
    {
        public DuplicatedRuleException()
             : base("This rule already exists") { }

    }
}
