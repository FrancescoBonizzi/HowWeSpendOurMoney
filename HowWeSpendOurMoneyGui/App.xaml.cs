using Castle.Windsor;
using HowWeSpendOurMoneyGui.Services;
using HowWeSpendOurMoneyGui.ViewModel;
using System.Windows;

namespace HowWeSpendOurMoneyGui
{
    public partial class App : Application
    {
        private WindsorContainer _dependeciesContainer;

        void App_Startup(object sender, StartupEventArgs e)
        {
            _dependeciesContainer = new WindsorContainer();
            _dependeciesContainer.Install(new DependenciesInstaller());
            var window = new MainWindow(_dependeciesContainer.Resolve<MainWindowViewModel>());
            window.Show();
        }
    }
}
