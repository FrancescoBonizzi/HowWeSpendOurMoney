using System;

namespace HowWeSpendOurMoney.Domain
{
    public class TagStatistics
    {
        public string TagName { get; }
        public double TagPercentage { get; }
        public decimal TotalExpense { get; }
        public decimal AvgExpense { get; }

        public TagStatistics(
            string tagName, 
            double tagPercentage, 
            decimal totalExpense,
            decimal avgExpense)
        {
            TagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
            TagPercentage = tagPercentage;
            TotalExpense = totalExpense;
            AvgExpense = avgExpense;
        }
    }
}
