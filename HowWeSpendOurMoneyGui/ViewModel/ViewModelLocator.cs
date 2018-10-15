using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using HowWeSpendOurMoney;
using HowWeSpendOurMoney.Infrastructure;

namespace HowWeSpendOurMoneyGui.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ITagsRepository, InMemoryTagsRepository>();
            SimpleIoc.Default.Register<IMoneyTransactionsImporter, MoneyTransactionsImporter>();
            SimpleIoc.Default.Register<IMoneyTransactionsRepository, InMemoryMoneyTransactionsRepository>();
            SimpleIoc.Default.Register<IImportingRules, InMemoryImportingRules>();
            SimpleIoc.Default.Register<IMoneyTransactionsAnalyzer, MoneyTransactionsAnalyzer>();
            SimpleIoc.Default.Register<IAnalysysRepository, InMemoryAnalysysRepository>();

            // TODO! Voglio che siano transient
            SimpleIoc.Default.Register<AnalysisViewModel>();
            SimpleIoc.Default.Register<MainWindowViewModel>();
        }

        public MainWindowViewModel MainWindowViewModel
            => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public AnalysisViewModel AnalysisViewModel
            => ServiceLocator.Current.GetInstance<AnalysisViewModel>();
    }
}
