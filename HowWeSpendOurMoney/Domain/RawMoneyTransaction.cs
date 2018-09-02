using System;

namespace HowWeSpendOurMoney.Domain
{
    public class RawMoneyTransaction
    {
        public DateTimeOffset ImportDate { get; set; }
        public string RawTransaction { get; set; }
    }
}
