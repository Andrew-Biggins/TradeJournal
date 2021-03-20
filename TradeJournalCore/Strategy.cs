using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Strategy : DatabaseSelectable, ISelectableTradeField
    {
        public Strategy(string name) : base(name)
        {
        }
    }
}
