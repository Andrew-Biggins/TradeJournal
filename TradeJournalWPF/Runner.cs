using Common.Interfaces;
using System;
using System.Windows;
using Common;
using TradeJournalCore.Interfaces;
using TradeJournalWPF.Windows;

namespace TradeJournalWPF
{
    public sealed class Runner : IRunner
    {
        public Runner(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void GetTradeDetails(object sender)
        {
            _context.Send(_ =>
            {
                var window = new AddTradeWindow { DataContext = sender };
                window.ShowDialog();
            });

        }

        public void GetNewName(object viewModel, string title)
        {
            _context.Send(_ =>
            {
                var window = new GetNameWindow { DataContext = viewModel, Title = title};
                window.ShowDialog();
            });
        }

        public bool RunForResult(object sender, Message message)
        {
            var result = false;

            _context.Send(_ =>
            {
                result = MessageBox.Show(new Window(), message.ContentKey, message.NameKey,
                             MessageBoxButton.YesNo,
                             MessageBoxImage.Information) == MessageBoxResult.Yes;
            });

            return result;
        }

        private readonly IContext _context;
    }
}
