using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Factory;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HowWeSpendOurMoney
{
    public class MoneyTransactionsAnalyzer : IMoneyTransactionsAnalyzer
    {
        public IEnumerable<PeriodAnalysis> Analyze(IEnumerable<MoneyTransaction> allTransactions)
        {
            var yearMonthAnalysis = new List<PeriodAnalysis>();

            var allYears = allTransactions
                .Select(t => t.Date.Year)
                .Distinct()
                .OrderBy(y => y);
            foreach (var year in allYears)
            {
                yearMonthAnalysis.Add(GetAnalysis(
                    allTransactions,
                    DateTimeOffsetFactory.CreateAtFirstDayOfTheYear(year),
                    DateTimeOffsetFactory.CreateAtFirstDayOfTheYear(year + 1)));
            }

            var allMonths = allTransactions
                .Select(t => (Year: t.Date.Year, Month: t.Date.Month))
                .Distinct()
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month);
            foreach (var (Year, Month) in allMonths)
            {
                yearMonthAnalysis.Add(GetAnalysis(
                    allTransactions,
                    DateTimeOffsetFactory.CreateAtFirstDayOfTheMonth(Month, Year),
                    DateTimeOffsetFactory.CreateAtFirstDayOfTheMonth(1 + Month % 12, Year)));
            }

            return yearMonthAnalysis;
        }

        private PeriodAnalysis GetAnalysis(
            IEnumerable<MoneyTransaction> allTransactions,
            DateTimeOffset from,
            DateTimeOffset to)
        {
            var thisPeriodTransactions = allTransactions.Where(t => t.Date >= from && t.Date < to);

            string periodName;

            // If the period is "years", I calculate the AVG value considering the number of months,
            // else the number of days
            int avgDivisor;
            TimeSpan intervalBetweenDates = to - from;
            if (intervalBetweenDates > TimeSpan.FromDays(31))
            {
                periodName = $"Year: {from.Year}";
                avgDivisor = ((to.Year - from.Year) * 12) + (to.Month - from.Month);
            }
            else
            {
                periodName = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(from.Month)} {from.Year}";
                avgDivisor = (int)(intervalBetweenDates).TotalDays;
            }

            decimal totalExpense = Math.Abs(Math.Round(thisPeriodTransactions.Sum(t => t.Amount), 2));

            var tagStatistics = new List<TagStatistics>();
            foreach (var tag in thisPeriodTransactions.SelectMany(t => t.Tags).Distinct())
            {
                var transactionsWithThisTag = thisPeriodTransactions.Where(t => t.Tags.Contains(tag));
                decimal totalExpenseForThisTag = Math.Abs(Math.Round(transactionsWithThisTag.Sum(t => t.Amount), 2));
                decimal avgExpenseForThisTag = Math.Abs(Math.Round(totalExpenseForThisTag / avgDivisor, 2));
                double moneyPercentageForThisTag = (double)(Math.Round((totalExpenseForThisTag / totalExpense) * 100, 2));

                tagStatistics.Add(new TagStatistics(
                    tag,
                    moneyPercentageForThisTag,
                    totalExpenseForThisTag,
                    avgExpenseForThisTag));
            }

            tagStatistics = tagStatistics.OrderByDescending(t => t.TagPercentage).ToList();

            return new PeriodAnalysis(
                from, to,
                periodName,
                totalExpense,
                tagStatistics);
        }
    }
}
