using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Strategy : ISelectable
    {
        public string Name { get; }

        public bool IsSelected { get; set; }

        public Strategy(string name)
        {
            Name = name;
        }
    }
}
