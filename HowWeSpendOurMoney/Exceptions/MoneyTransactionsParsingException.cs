using System;

namespace HowWeSpendOurMoney.Exceptions
{
    public class MoneyTransactionsParsingException : Exception
    {
        public MoneyTransactionsParsingException(string message) 
            : base(message) { }
    }
}
