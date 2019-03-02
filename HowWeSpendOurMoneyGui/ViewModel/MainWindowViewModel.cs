using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HowWeSpendOurMoney.Domain;
using HowWeSpendOurMoney.Infrastructure;
using HowWeSpendOurMoney.TransactionsParsers;
using HowWeSpendOurMoneyGui.Services.Infrastructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HowWeSpendOurMoneyGui.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MoneyTransaction> MoneyTransactions { get; private set; }
        public ObservableCollection<string> Tags { get; private set; }
        private IEnumerable<string> _rawMoneyTransactions;

        public ICommand ParseBPMCommand { get; }
        public ICommand INGCommand { get; }
        public ICommand AddTagToSelectedTransaction { get; }
        public ICommand RemoveTagFromSelectedTransaction { get; }
        public ICommand ExitApplicationCommand { get; }
        public ICommand ImportAllCommand { get; }
        public ICommand ClearAllCommand { get; }
        public ICommand ShowAnalysisCommand { get; }

        private readonly ITagsRepository _tagsRepository;
        private readonly IMoneyTransactionsImporter _moneyTransactionsImporter;
        private readonly IImportingRules _importingRules;
        private readonly IDialogsManager _dialogsManager;
        private readonly INavigator _navigator;

        public Task Initialization { get; private set; } // TODO: finché non è completato non mostrare la form

        public MainWindowViewModel(
            ITagsRepository tagsRepository,
            IMoneyTransactionsImporter moneyTransactionsImporter,
            IImportingRules importingRules,
            IDialogsManager dialogsManager,
            INavigator navigator)
        {
            _tagsRepository = tagsRepository ?? throw new ArgumentNullException(nameof(tagsRepository));
            _moneyTransactionsImporter = moneyTransactionsImporter ?? throw new ArgumentNullException(nameof(moneyTransactionsImporter));
            _importingRules = importingRules ?? throw new ArgumentNullException(nameof(importingRules));
            _dialogsManager = dialogsManager ?? throw new ArgumentNullException(nameof(dialogsManager));
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

            MoneyTransactions = new ObservableCollection<MoneyTransaction>();
            ParseBPMCommand = new RelayCommand(ImportBPMCommandMethod);
            INGCommand = new RelayCommand(ImportINGCommandMethod);

            ExitApplicationCommand = new RelayCommand(() => System.Windows.Application.Current.Shutdown());
            ImportAllCommand = new RelayCommand(ImportAllMethod, () => MoneyTransactions.Any() && !MoneyTransactions.Any(m => !m.Tags.Any()));
            ClearAllCommand = new RelayCommand(() => MoneyTransactions.Clear(), () => MoneyTransactions.Any());

            ShowAnalysisCommand = new RelayCommand(() => _navigator.ShowFormAnalysisWindow());

            AddTagToSelectedTransaction = new RelayCommand(
                AddTagToSelectedTransactionMethod,
                () =>
                {
                    if (SelectedTransaction == null)
                        return false;

                    if (SelectedTagListTag == null)
                        return false;

                    if (SelectedTransaction.Tags.Contains(SelectedTagListTag))
                        return false;

                    return true;
                });

            RemoveTagFromSelectedTransaction = new RelayCommand(
                RemoveTagFromSelectedTransactionMethod,
                () =>
                {
                    if (SelectedTransaction == null)
                        return false;

                    if (SelectedTransactionTag == null)
                        return false;

                    if (!SelectedTransaction.Tags.Contains(SelectedTransactionTag))
                        return false;

                    return true;
                });
            
            Initialization = InitializeAsync();
        }

        private async void ImportAllMethod()
        {
            await _moneyTransactionsImporter.ImportTransactions(_rawMoneyTransactions, MoneyTransactions);
            MoneyTransactions.Clear();
        }

        private async Task InitializeAsync()
        {
            Tags = new ObservableCollection<string>(await _tagsRepository.GetAll());
            RaisePropertyChanged(() => Tags);
        }

        private void AddTagToSelectedTransactionMethod()
        {
            SelectedTransaction.Tags.Add(SelectedTagListTag);
        }

        private void RemoveTagFromSelectedTransactionMethod()
        {
            SelectedTransaction.Tags.Remove(SelectedTransactionTag);
        }

        private void ImportBPMCommandMethod()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
                return;

            _rawMoneyTransactions = File.ReadAllLines(openFileDialog.FileName);
            var parser = new BpmCsvTransactionParser();
            var parsedTransactions = parser.ParseTransactions(_rawMoneyTransactions);

            foreach (var parsedTransaction in parsedTransactions)
                _importingRules.ApplyRules(parsedTransaction);
            
            MoneyTransactions = new ObservableCollection<MoneyTransaction>(parsedTransactions.OrderBy(p => p.Tags.Count));
            RaisePropertyChanged(() => MoneyTransactions);
        }

        private void ImportINGCommandMethod()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
                return;

            _rawMoneyTransactions = File.ReadAllLines(openFileDialog.FileName);
            var parser = new IngXlsHtmlTransactionParser();
            var parsedTransactions = parser.ParseTransactions(_rawMoneyTransactions);

            foreach (var parsedTransaction in parsedTransactions)
                _importingRules.ApplyRules(parsedTransaction);
            
            MoneyTransactions = new ObservableCollection<MoneyTransaction>(parsedTransactions.OrderBy(p => p.Tags.Count));
            RaisePropertyChanged(() => MoneyTransactions);
        }

        private MoneyTransaction _selectedTransaction;
        public MoneyTransaction SelectedTransaction
        {
            get
            {
                return _selectedTransaction;
            }
            set
            {
                _selectedTransaction = value;
                RaisePropertyChanged(nameof(SelectedTransaction));
            }
        }

        private string _selectedTransactionTag;
        public string SelectedTransactionTag
        {
            get
            {
                return _selectedTransactionTag;
            }
            set
            {
                _selectedTransactionTag = value;
                RaisePropertyChanged(nameof(SelectedTransactionTag));

            }
        }

        private string _selectedTagListTag;
        public string SelectedTagListTag
        {
            get
            {
                return _selectedTagListTag;
            }
            set
            {
                _selectedTagListTag = value;
                RaisePropertyChanged(nameof(SelectedTagListTag));
            }
        }

    }
}
