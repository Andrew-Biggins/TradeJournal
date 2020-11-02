using Common.Interfaces;
using System;
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

        private readonly IContext _context;
    }
}
