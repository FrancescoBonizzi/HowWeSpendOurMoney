namespace HowWeSpendOurMoneyGui.Services.Infrastructure
{
    public interface IDialogsManager
    {
        void ShowInformation(string message);
        void ShowError(string message);
    }
}
