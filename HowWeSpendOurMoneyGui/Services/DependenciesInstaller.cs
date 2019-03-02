using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HowWeSpendOurMoney;
using HowWeSpendOurMoney.Infrastructure;
using HowWeSpendOurMoneyGui.Services.Infrastructure;
using HowWeSpendOurMoneyGui.ViewModel;
using System;
using System.IO;

namespace HowWeSpendOurMoneyGui.Services
{
    public class DependenciesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITagsRepository>().ImplementedBy<InMemoryTagsRepository>().LifestyleSingleton());
            container.Register(Component.For<IMoneyTransactionsImporter>().ImplementedBy<MoneyTransactionsImporter>().LifestyleSingleton());
            container.Register(Component.For<IMoneyTransactionsRepository>().ImplementedBy<InMemoryMoneyTransactionsRepository>().LifestyleSingleton());
            container.Register(Component.For<IMoneyTransactionsAnalyzer>().ImplementedBy<MoneyTransactionsAnalyzer>().LifestyleSingleton());
            container.Register(Component.For<IAnalysysRepository>().ImplementedBy<InMemoryAnalysysRepository>().LifestyleSingleton());
            container.Register(Component.For<IImportingRules>().Instance(new JsonFileImportingRules(GetJsonFileForRules())));
            container.Register(Component.For<IDialogsManager>().ImplementedBy<WPFDialogsManager>().LifestyleSingleton());
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            container.Register(Component.For<INavigator>().ImplementedBy<WPFWindowsNavigator>().LifestyleSingleton());
            container.Register(Component.For<AnalysisViewModel>().LifestyleTransient());
            container.Register(Component.For<MainWindowViewModel>().LifestyleTransient());
        }

        private static string GetJsonFileForRules()
        {
            const string _fileName = "rules.json";
            const string _applicationFolder = "HowWeSpendOurMoney";

            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                _applicationFolder,
                _fileName);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    _applicationFolder));
            }

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            filePath = Path.GetFullPath(filePath);
            string fileExtension = Path.GetExtension(filePath);
            if (!string.Equals(fileExtension, ".json", StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException($"FilePath extension should be json, you gave: '{fileExtension}'", nameof(filePath));

            // If exists the example file, and doesn't exists the official application file, move it to the application folder
            string exampleFile = "ExampleFiles/rules.json";
            if (!File.Exists(filePath) && File.Exists("ExampleFiles/rules.json"))
            {
                File.Copy(exampleFile, filePath);
            }

            return filePath;
        }
    }
}