using HowWeSpendOurMoneyGui.Services.Infrastructure;
using System.Windows;

namespace HowWeSpendOurMoneyGui.Services
{
    public class WPFDialogsManager : IDialogsManager
    {
        public void ShowError(string message)
            => MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowInformation(string message)
            => MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);

    }
}
