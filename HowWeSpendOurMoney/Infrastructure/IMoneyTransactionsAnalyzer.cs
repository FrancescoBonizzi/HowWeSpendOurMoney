using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IMoneyTransactionsAnalyzer
    {
        IEnumerable<PeriodAnalysis> Analyze(IEnumerable<MoneyTransaction> allTransactions);
    }
}
