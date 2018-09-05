using GalaSoft.MvvmLight;
using HowWeSpendOurMoney.Infrastructure;
using HowWeSpendOurMoneyGui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowWeSpendOurMoneyGui.ViewModel
{
    public class AnalysisViewModel : ViewModelBase
    {
        private readonly IAnalysysRepository _analysysRepository;
        private readonly Task _loadingTask;

        public IEnumerable<YearsSplitPeriodAnalyses> PeriodAnalyses { get; private set; }
        
        public AnalysisViewModel(IAnalysysRepository analysysRepository)
        {
            _analysysRepository = analysysRepository ?? throw new ArgumentNullException(nameof(analysysRepository));
            _loadingTask = LoadAnalysys();
        }

        private async Task LoadAnalysys()
        {
            var analyses = await _analysysRepository.GetAll();
            PeriodAnalyses = analyses
                .GroupBy(a => a.From.Year)
                .Select(g => new YearsSplitPeriodAnalyses(
                    g.Key,
                    g.Select(p => p)));
            RaisePropertyChanged(() => PeriodAnalyses);
        }

    }
}
