using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class MoneyTransactionsImporter : IMoneyTransactionsImporter
    {
        private readonly IMoneyTransactionsRepository _moneyTransactionsRepository;
        private readonly IMoneyTransactionsAnalyzer _moneyTransactionsAnalyzer;
        private readonly IAnalysysRepository _analysysRepository;

        public MoneyTransactionsImporter(
            IMoneyTransactionsRepository moneyTransactionsRepository,
            IMoneyTransactionsAnalyzer moneyTransactionsAnalyzer,
            IAnalysysRepository analysysRepository)
        {
            _moneyTransactionsRepository = moneyTransactionsRepository ?? throw new ArgumentNullException(nameof(moneyTransactionsRepository));
            _moneyTransactionsAnalyzer = moneyTransactionsAnalyzer ?? throw new ArgumentNullException(nameof(moneyTransactionsAnalyzer));
            _analysysRepository = analysysRepository ?? throw new ArgumentNullException(nameof(analysysRepository));
        }

        public async Task ImportTransactions(
            IEnumerable<string> rawMoneyTransactions,
            IEnumerable<MoneyTransaction> parsedMoneyTransactions)
        {
            await _moneyTransactionsRepository.StoreImportedTransactionsRaw(rawMoneyTransactions);
            await _moneyTransactionsRepository.StoreParsedTransactions(parsedMoneyTransactions);
            var analysys = _moneyTransactionsAnalyzer.Analyze(parsedMoneyTransactions);
            await _analysysRepository.Store(analysys);
        }
    }
}
