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
            foreach(var (Year, Month) in allMonths)
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

            decimal totalExpense = Math.Abs(Math.Round(thisPeriodTransactions.Sum(t => t.Amount), 2));

            List<TagStatistics> tagStatistics = new List<TagStatistics>();
            foreach (var tag in thisPeriodTransactions.SelectMany(t => t.Tags).Distinct())
            {
                // TODO migliorare con una hashtable o simili il contains. Migliora davvero con le stringhe? Prova con linqpad
                var transactionsWithThisTag = thisPeriodTransactions.Where(t => t.Tags.Contains(tag));
                decimal totalExpenseForThisTag = Math.Abs(Math.Round(transactionsWithThisTag.Sum(t => t.Amount), 2));

                // TODO: sbagliato! Questa è la media per numero di transazioni, a me serve la media per mese e per anno
                // Avg ha senso solo negli anni se divido per mesi
                // e in mesi se divido per giorni
                decimal avgExpenseForThisTag = Math.Abs(Math.Round(transactionsWithThisTag.Average(t => t.Amount), 2));


                double moneyPercentageForThisTag = (double)(Math.Round((totalExpenseForThisTag / totalExpense) * 100, 2));
                
                tagStatistics.Add(new TagStatistics(
                    tag,
                    moneyPercentageForThisTag,
                    totalExpenseForThisTag,
                    avgExpenseForThisTag));
            }

            tagStatistics = tagStatistics.OrderByDescending(t => t.TagPercentage).ToList();

            string periodName;
            if (to - from > TimeSpan.FromDays(60))
                periodName = $"Year: {from.Year}";
            else periodName = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(from.Month)} {from.Year}";

            return new PeriodAnalysis(
                from, to,
                periodName,
                totalExpense,
                tagStatistics);
        }
    }
}

// https://lvcharts.net/App/examples/v1/wpf/Pie%20Chart