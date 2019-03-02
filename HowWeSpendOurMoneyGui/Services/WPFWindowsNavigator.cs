using Castle.Windsor;
using HowWeSpendOurMoneyGui.Services.Infrastructure;
using HowWeSpendOurMoneyGui.ViewModel;
using System;

namespace HowWeSpendOurMoneyGui.Services
{
    public class WPFWindowsNavigator : INavigator
    {
        private readonly IWindsorContainer _windsorContainer;

        public WPFWindowsNavigator(IWindsorContainer windsorContainer)
        {
            _windsorContainer = windsorContainer ?? throw new ArgumentNullException(nameof(windsorContainer));
        }

        public void ShowFormAnalysisWindow()
        {
            var window = new AnalysisWindow(_windsorContainer.Resolve<AnalysisViewModel>());
            window.Show();
        }
    }
}
