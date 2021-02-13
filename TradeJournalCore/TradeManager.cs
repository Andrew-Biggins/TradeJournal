using Common;
using Common.Optional;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.SelectableFactory;
using static TradeJournalCore.TradeFilterer;

namespace TradeJournalCore
{
    public sealed class TradeManager : ITradeManager, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler DateRangeChanged;

        public ObservableCollection<ITrade> Trades { get; private set; } = new ObservableCollection<ITrade>();

        public ITrade SelectedTrade { get; set; }

        public IFilters Filters { get; set; } = new Filters(GetDefaultMarkets(), GetDefaultStrategies(),
            GetAssetTypes(), GetDays(), DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0,
            9999, TradeStatus.Both, TradeDirection.Both);

        public void ReadInTrades()
        {
            _unfilteredTrades = DataConnection.GetAllTrades();
            UpdateDateRange();
            FilterTrades(Filters);
        }

        public void AddNewTrade(TradeDetailsViewModel tradeDetails)
        {
            var close = GetCloseExecution(tradeDetails);

            var trade = new Trade(tradeDetails.SelectedMarket, tradeDetails.SelectedStrategy,
                new Levels(tradeDetails.Levels.Entry, tradeDetails.Levels.Stop, tradeDetails.Levels.Target),
                new Execution(tradeDetails.Open.Level, tradeDetails.Open.DateTime, tradeDetails.Open.Size), close,
                (tradeDetails.MaxAdverse, tradeDetails.MaxFavourable), tradeDetails.SelectedEntryOrderType);

            _unfilteredTrades.Add(trade);
            DataConnection.AddTrade(trade);
            UpdateDateRange(tradeDetails.Open.DateTime);
            FilterTrades(Filters);
        }

        public void RemoveTrade()
        {
            DataConnection.RemoveTrade(SelectedTrade.Id);
            _unfilteredTrades.Remove(SelectedTrade);
            Trades.Remove(SelectedTrade);
            PropertyChanged.Raise(this, nameof(Trades));
            UpdateDateRange();
        }

        public void FilterTrades(IFilters filters)
        {
            Filters = filters;
            var assetClassesRemoved = RemoveUnselectedAssetClasses(_unfilteredTrades, filters.AssetClasses);
            var marketsRemoved = RemoveUnselectedMarkets(assetClassesRemoved, filters.Markets);
            var strategiesRemoved = RemoveUnselectedStrategies(marketsRemoved, filters.Strategies);
            var daysRemoved = RemoveUnselectedDays(strategiesRemoved, filters.Days);
            var datesRemoved = RemoveTradesOutsideDateRange(daysRemoved, filters.StartDate, filters.EndDate);
            var timesRemoved = RemoveTradesOutsideTimeRange(datesRemoved, filters.StartTime, filters.EndTime);
            var riskRewardRatiosRemoved = RemoveTradesOutsideRiskRewardRatioRange(timesRemoved,
                filters.MinRiskRewardRatio, filters.MaxRiskRewardRatio);
            var statusesRemoved = RemoveUnselectedTradeStatuses(riskRewardRatiosRemoved, filters.Status);
            Trades = RemoveUnselectedTradeDirections(statusesRemoved, filters.Direction);
            PropertyChanged.Raise(this, nameof(Trades));
        }

        public (DateTime, DateTime) GetDateRange()
        {
            return (_startDate, _endDate);
        }

        public void ClearAll()
        {
            DataConnection.ClearTrades();
            _unfilteredTrades.Clear();
            Trades.Clear();
            PropertyChanged.Raise(this, nameof(Trades));
            _startDate = DateTime.MaxValue;
            _endDate = DateTime.MinValue;
        }

        private void UpdateDateRange(DateTime date)
        {
            var dateRangeChanged = false;

            if (date < _startDate)
            {
                _startDate = date.Date;
                dateRangeChanged = true;
            }

            if (date > _endDate)
            {
                _endDate = date.Date;
                dateRangeChanged = true;
            }

            if (dateRangeChanged)
            {
                DateRangeChanged.Raise(this);
            }
        }

        private void UpdateDateRange()
        {
            _startDate = DateTime.MaxValue;
            _endDate = DateTime.MinValue;

            foreach (var trade in _unfilteredTrades)
            {
                if (trade.Open.DateTime < _startDate)
                {
                    _startDate = trade.Open.DateTime.Date;
                }

                if (trade.Open.DateTime > _endDate)
                {
                    _endDate = trade.Open.DateTime.Date;
                }
            }

            DateRangeChanged.Raise(this);
        }

        private static Optional<Execution> GetCloseExecution(TradeDetailsViewModel tradeDetails)
        {
            var close = new Execution(0, tradeDetails.CloseDateTime, 0);
            var fieldIsEmpty = false;

            tradeDetails.CloseLevel.IfExistsThen(x => { close.Level = x; }).IfEmpty(() => fieldIsEmpty = true);

            return fieldIsEmpty ? Option.None<Execution>() : Option.Some(close);
        }

        private IList<ITrade> _unfilteredTrades = new List<ITrade>();
        private DateTime _startDate = DateTime.MaxValue;
        private DateTime _endDate = DateTime.MinValue;
    }
}
