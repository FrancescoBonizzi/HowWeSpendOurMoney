using System.Windows;

namespace HowWeSpendOurMoneyGui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var w = new AnalysisWindow();
            w.Show();
        }
    }
}
