using TradeJournalCore.Interfaces;

namespace TradeJournalCore
{
    public sealed class Market : ISelectable
    {
        public string Name { get; }

        public bool IsSelected { get; set; }

        public Market(string name)
        {
            Name = name;
        }
    }
}
