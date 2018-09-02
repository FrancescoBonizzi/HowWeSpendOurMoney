using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IMoneyTransactionsParser
    {
        IEnumerable<MoneyTransaction> ParseTransactions(IEnumerable<string> moneyTransactionsRaw);
    }
}
