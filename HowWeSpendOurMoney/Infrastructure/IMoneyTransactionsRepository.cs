using HowWeSpendOurMoney.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney.Infrastructure
{
    public interface IMoneyTransactionsRepository
    {
        Task<IEnumerable<RawMoneyTransaction>> GetRawImportedTransactions();
        Task<IEnumerable<MoneyTransaction>> GetParsedTransactions();
        Task StoreImportedTransactionsRaw(IEnumerable<string> rawMoneyTransactions);
        Task StoreParsedTransactions(IEnumerable<MoneyTransaction> parsedMoneyTransactions);
    }
}
