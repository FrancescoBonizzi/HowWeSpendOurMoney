using HowWeSpendOurMoneyGui.ViewModel;
using System.Windows;

namespace HowWeSpendOurMoneyGui
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }
    }
}
