﻿using Common;

namespace TradeJournalCore.Interfaces
{
    public interface IRunner
    {
        void GetTradeDetails(object sender);

        void GetNewName(object viewModel, string title);

        void GetNewMarket(object viewModel, string title);

        bool RunForResult(object sender, Message message);

        void ShowGraphWindow(object sender);

        void ShowStatsWindow(object sender);

    }
}