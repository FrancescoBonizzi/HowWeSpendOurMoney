using HowWeSpendOurMoneyGui.ViewModel;
using System.Windows;

namespace HowWeSpendOurMoneyGui
{
    public partial class AnalysisWindow : Window
    {
        public AnalysisWindow(AnalysisViewModel analysisViewModel)
        {
            InitializeComponent();
            DataContext = analysisViewModel;
        }
    }
}
