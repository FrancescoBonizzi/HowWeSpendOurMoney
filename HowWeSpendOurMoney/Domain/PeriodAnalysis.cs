using System;
using System.Collections.Generic;

namespace HowWeSpendOurMoney.Domain
{
    public class PeriodAnalysis
    {
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }
        public string PeriodName { get; }
        public decimal TotalExpense { get; }
        public IEnumerable<TagStatistics> TagStatistics { get; }

        public PeriodAnalysis(
            DateTimeOffset from, DateTimeOffset to, 
            string periodName,
            decimal totalExpense, 
            IEnumerable<TagStatistics> tagStatistics)
        {
            From = from;
            To = to;
            PeriodName = periodName;
            TotalExpense = totalExpense;
            TagStatistics = tagStatistics ?? throw new ArgumentNullException(nameof(tagStatistics));
        }
    }
}
