using GalaSoft.MvvmLight;
using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HowWeSpendOurMoneyGui.ViewModel
{
    public class AnalysisViewModel : ViewModelBase
    {
        private readonly IAnalysysRepository _analysysRepository;
        private readonly Task _loadingTask;

        public IEnumerable<PeriodAnalysis> PeriodAnalyses { get; private set; }

        public AnalysisViewModel(IAnalysysRepository analysysRepository)
        {
            _analysysRepository = analysysRepository ?? throw new ArgumentNullException(nameof(analysysRepository));
            _loadingTask = LoadAnalysys();
        }

        private async Task LoadAnalysys()
        {
            PeriodAnalyses = await _analysysRepository.GetAll();
            RaisePropertyChanged(() => PeriodAnalyses);
        }

    }
}
