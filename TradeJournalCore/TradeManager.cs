using System;
using Common;
using Common.Optional;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TradeJournalCore.Interfaces;
using TradeJournalCore.ViewModels;
using static TradeJournalCore.TradeFilterer;
using static TradeJournalCore.SelectableFactory;

namespace TradeJournalCore
{
    public sealed class TradeManager : ITradeManager, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler DateRangeChanged;

        public ObservableCollection<ITrade> Trades { get; private set; } = new ObservableCollection<ITrade>();

        public ITrade SelectedTrade { get; set; }

        public IFilters Filters { get; set; } = new Filters(GetDefaultMarkets(), GetDefaultStrategies(), GetAssetTypes(), GetDays(), DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue, DateTime.MaxValue, 0, 9999,TradeStatus.Both, TradeDirection.Both);

        public void ReadInTrades()
        {
            var openTime = DateTime.Today;

            for (int i = 0; i < 10000; i++)
            {
                openTime = openTime.AddDays(1);
                _unfilteredTrades.Add(new Trade(new Market("USDJPY", AssetClass.Currencies), new Strategy("Triangle"),
                    new Levels(1.045, 1.030, 1.060), new Execution(1.045, openTime, 1),
                    Option.Some(new Execution(1.06, new DateTime(2021, 01, 01, 15, 23, 00), 1)), (Option.None<double>(), Option.None<double>())));
            }


            _unfilteredTrades.Add(new Trade(new Market("USDJPY", AssetClass.Currencies), new Strategy("Triangle"),
                new Levels(1.045, 1.030, 1.060), new Execution(1.045, new DateTime(2021, 01, 01, 12, 23, 00), 1),
                Option.Some(new Execution(1.06, new DateTime(2021, 01, 01, 15, 23, 00), 1)), (Option.None<double>(), Option.None<double>())));

            _unfilteredTrades.Add(new Trade(new Market("USDJPY", AssetClass.Currencies), new Strategy("Triangle"),
                new Levels(1.045, 1.030, 1.060), new Execution(1.045, new DateTime(2021, 01, 21, 12, 23, 00), 1),
                Option.Some(new Execution(1.16, new DateTime(2021, 01, 22, 15, 23, 00), 1)), (Option.None<double>(), Option.None<double>())));

            _unfilteredTrades.Add(new Trade(new Market("Gold", AssetClass.Commodities), new Strategy("Gap fill"),
                new Levels(1.045, 1.030, 1.060), new Execution(1.045, new DateTime(2021, 01, 11, 12, 23, 00), 1),
                Option.Some(new Execution(1.06, new DateTime(2021, 01, 15, 15, 23, 00), 1)), (Option.None<double>(), Option.None<double>())));


            UpdateDateRange();
            FilterTrades(Filters);
        }

        public void AddNewTrade(TradeDetailsViewModel tradeDetails)
        {
            var close = GetCloseExecution(tradeDetails);

            var trade = new Trade(tradeDetails.SelectedMarket, tradeDetails.SelectedStrategy,
                new Levels(tradeDetails.Levels.Entry, tradeDetails.Levels.Stop, tradeDetails.Levels.Target),
                new Execution(tradeDetails.Open.Level, tradeDetails.Open.DateTime, tradeDetails.Open.Size), close,
                (tradeDetails.MaxAdverse, tradeDetails.MaxFavourable));

            _unfilteredTrades.Add(trade);
            UpdateDateRange(tradeDetails.Open.DateTime);
            FilterTrades(Filters);
        }

        public void RemoveTrade()
        {
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

        private readonly IList<ITrade> _unfilteredTrades = new List<ITrade>();
        private DateTime _startDate = DateTime.MaxValue;
        private DateTime _endDate = DateTime.MinValue;
    }
}
