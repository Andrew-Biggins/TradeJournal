using Common;
using Common.Optional;

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

        void ShowUploadTradesWindow(object sender);

        Optional<string> OpenSaveDialog(object sender, string fileName, string filter);
    }
}