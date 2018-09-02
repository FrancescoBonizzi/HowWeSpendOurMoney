using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IMoneyTransactionsImporter
    {
        Task ImportTransactions(
            IEnumerable<string> rawMoneyTransactions,
            IEnumerable<MoneyTransaction> parsedMoneyTransactions);
    }
}
