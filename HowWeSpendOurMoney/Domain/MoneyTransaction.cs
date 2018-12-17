using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HowWeSpendOurMoney.Domain
{
    public class MoneyTransaction
    {
        public DateTime AccountingDate { get; }
        public DateTime MovementDate { get; }
        public decimal Amount { get; }
        public string Currency { get; }
        public string Description { get; }
        public ICollection<string> Tags { get; set; }
        
        public MoneyTransaction(
            DateTime accountingDate,
            DateTime movementDate,
            decimal amount,
            string currency,
            string description,
            ICollection<string> tags)
        {
            AccountingDate = accountingDate;
            MovementDate = movementDate;
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        public MoneyTransaction(
            DateTime accountingDate,
            decimal amount,
            string currency,
            string description)
            : this(accountingDate, accountingDate, amount, currency, description, new ObservableCollection<string>())
        { }
    }

    public class MoneyTransactionBuilder
    {
        private DateTime _accountingDate;
        private DateTime _movementDate;
        private decimal _amount;
        private string _currency;
        private string _description;
        private readonly List<string> _tags;
        private string _movementType;

        private MoneyTransactionBuilder()
        {
            _tags = new List<string>();
        }
        public MoneyTransactionBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public MoneyTransactionBuilder WithMovementType(string movType)
        {
            _movementType = movType;
            return this;
        }

        public MoneyTransactionBuilder InThisDates(DateTime accountingDate, DateTime movementDate)
        {
            _movementDate = movementDate;
            _accountingDate = accountingDate;
            return this;
        }

        public MoneyTransactionBuilder InThisDates(string accountingDate, string movementDate, string dateFormat)
        {
            _movementDate = DateTime.ParseExact(movementDate, dateFormat, new System.Globalization.CultureInfo("en-US"));
            _accountingDate = DateTime.ParseExact(accountingDate, dateFormat, new System.Globalization.CultureInfo("en-US"));
            return this;
        }

        public MoneyTransactionBuilder WithThisAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public MoneyTransactionBuilder WithThisTotal(string totalString, string cultureId)
        {
            _amount = Decimal.Parse(totalString, new System.Globalization.CultureInfo(cultureId));
            return this;
        }

        public MoneyTransactionBuilder WithThisCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public MoneyTransactionBuilder WithTags(string[] tags)
        {
            _tags.AddRange(tags);
            return this;
        }

        public MoneyTransactionBuilder WithTag(string tag)
        {
            _tags.Add(tag);
            return this;
        }

        public MoneyTransaction Build()
        {
            return new MoneyTransaction
            (
                accountingDate: _accountingDate,
                movementDate : _movementDate,
                amount: _amount,
                currency: _currency,
                description: _description,
                tags: _tags
            );
        }

        public static MoneyTransactionBuilder Create()
        {
            return new MoneyTransactionBuilder();
        }
    }

}
