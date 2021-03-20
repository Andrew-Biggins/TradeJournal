using System;
using System.Collections.ObjectModel;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModelAdapters;

namespace TradeJournalCore
{
    public sealed class RiskManager
    {
        public IPiePlot PiePlot { get; } = new RiskPiePlot();

        public ObservableCollection<IRisk> Risks { get; } = new ObservableCollection<IRisk>();

        public RiskManager()
        {
            UpdateRisks();
        }

        public void UpdateRisks()
        {
            Risks.Clear();

            var assetClasses = (AssetClass[])Enum.GetValues(typeof(AssetClass));

            _total = 0.00;

            foreach (var assetClass in assetClasses)
            {
                var openTrades = DataConnection.GetOpenTradesByAssetClass(assetClass);

                var risk = 0.00;

                foreach (var trade in openTrades)
                {
                    risk += trade.Open.Size * (int) trade.Market.PipDivisor *
                            (Math.Abs(trade.Open.Level - trade.Levels.Stop));
                }

                _total += risk;

                Risks.Add(new Risk(assetClass.ToString(), risk));
            }

            Risks.Add(new Risk("Total", _total));

            if (_total > 0)
            {
                UpdatePercentages();
            }

            PiePlot.UpdateData(Risks);
        }

        private void UpdatePercentages()
        {
            foreach (var risk in Risks)
            {
                risk.Percentage = risk.Amount / _total;
            }
        }

        private double _total;
    }
}
