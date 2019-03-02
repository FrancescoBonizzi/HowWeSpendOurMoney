using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using HowWeSpendOurMoney;
using HowWeSpendOurMoney.Infrastructure;
using System;
using System.IO;

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
            SimpleIoc.Default.Register<IImportingRules>(() => new JsonFileImportingRules(GetJsonFileForRules()));
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
