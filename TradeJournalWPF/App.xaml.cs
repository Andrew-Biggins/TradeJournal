using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Common;
using Common.Interfaces;
using TradeJournalCore;
using TradeJournalCore.ViewModelAdapters;
using TradeJournalCore.ViewModels;
using TradeJournalWPF.Windows;

namespace TradeJournalWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox),
                UIElement.GotFocusEvent,
                new RoutedEventHandler(TextBox_GotFocus));

            var context = GetDispatcherContext();
            var runner = new Runner(context);
            Window window = new MainWindow();
            window.Show();
            var tradeManager = new TradeManager();
            var getNameViewModel = new GetNameViewModel();
            var addMarketViewModel = new AddMarketViewModel();
            var tradeDetailsViewModel = new TradeDetailsViewModel(runner, getNameViewModel, addMarketViewModel);
            var plot = new TradePlot();
            var mainViewModel = new MainWindowViewModel(runner, tradeManager, tradeDetailsViewModel, plot);
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

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }
    }
}
