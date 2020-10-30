using System.Windows;
using TradeJournalCore.ViewModels;

namespace TradeJournalWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.Show();
            var mainViewModel = new MainWindowViewModel();
            window.DataContext = mainViewModel;
        }
    }
}
