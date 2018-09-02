using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Exceptions;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowWeSpendOurMoney
{
    public class InMemoryMoneyTransactionsRepository : IMoneyTransactionsRepository
    {
        private readonly ICollection<RawMoneyTransaction> _rawMoneyTransactions;
        private readonly ICollection<MoneyTransaction> _parsedMoneyTransactions;

        public InMemoryMoneyTransactionsRepository()
        {
            _rawMoneyTransactions = new List<RawMoneyTransaction>();
            _parsedMoneyTransactions = new List<MoneyTransaction>();
        }

        public Task<IEnumerable<MoneyTransaction>> GetParsedTransactions()
            => Task.FromResult(_parsedMoneyTransactions.AsEnumerable());

        public Task<IEnumerable<RawMoneyTransaction>> GetRawImportedTransactions()
            => Task.FromResult(_rawMoneyTransactions.AsEnumerable());

        public Task StoreImportedTransactionsRaw(IEnumerable<string> rawMoneyTransactions)
        {
            DateTimeOffset importDate = DateTimeOffset.Now;
            foreach(var t in rawMoneyTransactions)
            {
                if (_rawMoneyTransactions.Where(r => r.RawTransaction == t).Any())
                    continue; // Idempotency

                _rawMoneyTransactions.Add(new RawMoneyTransaction()
                {
                    ImportDate = importDate,
                    RawTransaction = t
                });
            }

            return Task.CompletedTask;
        }

        public Task StoreParsedTransactions(IEnumerable<MoneyTransaction> parsedMoneyTransactions)
        {
            foreach (var p in parsedMoneyTransactions)
                _parsedMoneyTransactions.Add(p);

            return Task.CompletedTask;
        }
    }
}
