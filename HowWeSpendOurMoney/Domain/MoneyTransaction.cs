using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HowWeSpendOurMoney.Domain
{
    public class MoneyTransaction
    {
        public DateTime Date { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Description { get; }
        public ICollection<string> Tags { get; set; }
        
        public MoneyTransaction(
            DateTime date,
            decimal amount,
            string currency,
            string description,
            ICollection<string> tags)
        {
            Date = date;
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        public MoneyTransaction(
            DateTime date,
            decimal amount,
            string currency,
            string description)
            : this(date, amount, currency, description, new ObservableCollection<string>())
        { }
    }
}
