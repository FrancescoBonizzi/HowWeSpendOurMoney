using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Exceptions;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HowWeSpendOurMoney.TransactionsParsers
{
    // Bpm = Banca Popolare di Milano
    public class BpmCsvTransactionParser : IMoneyTransactionsParser
    {
        public IEnumerable<MoneyTransaction> ParseTransactions(IEnumerable<string> moneyTransactionsRaw)
        {
            if (moneyTransactionsRaw == null || !moneyTransactionsRaw.Any())
                throw new MoneyTransactionsParsingException("moneyTransactionsRaw cannot be null or empty");

            var moneyTransactions = new List<MoneyTransaction>();

            foreach (var rawTransaction in moneyTransactionsRaw.Skip(1))
            {
                var splitted = rawTransaction.Split(';');
                moneyTransactions.Add(new MoneyTransaction(
                    date: DateTime.Parse(splitted[0]),
                    amount: decimal.Parse(splitted[2]),
                    currency: splitted[3],
                    description: splitted[4]));
            }

            return moneyTransactions;
        }
    }
}
