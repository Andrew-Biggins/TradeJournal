using System.Threading;
using System.Windows;
using Common;
using Common.Interfaces;
using TradeJournalCore;
using TradeJournalCore.ViewModels;
using TradeJournalWPF.Windows;

namespace TradeJournalWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var context = GetDispatcherContext();
            var runner = new Runner(context);
            Window window = new MainWindow();
            window.Show();
            var tradeManager = new TradeManager();
            var getNameViewModel = new GetNameViewModel();
            var tradeDetailsViewModel = new TradeDetailsViewModel(runner, getNameViewModel);
            var mainViewModel = new MainWindowViewModel(runner, tradeManager, tradeDetailsViewModel);
            window.DataContext = mainViewModel;
        }

        private IContext GetDispatcherContext()
        {
            var dispatcherContext = Dispatcher?.Invoke(() => SynchronizationContext.Current);

            if (dispatcherContext == null)
            {
                throw new ThreadStateException("Could not get dispatcher synchronisation context.");
            }

            var context = new Context(dispatcherContext);
            return context;
        }
    }
}
