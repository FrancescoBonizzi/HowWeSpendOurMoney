using HowWeSpendOurMoney.Domain;
using System;
using System.Collections.Generic;

namespace HowWeSpendOurMoneyGui.Model
{
    public class YearsSplitPeriodAnalyses
    {
        public int Year { get; }
        public IEnumerable<PeriodAnalysis> PeriodAnalyses { get; }

        public YearsSplitPeriodAnalyses(int year, IEnumerable<PeriodAnalysis> periodAnalyses)
        {
            Year = year;
            PeriodAnalyses = periodAnalyses ?? throw new ArgumentNullException(nameof(periodAnalyses));
        }
    }
}
