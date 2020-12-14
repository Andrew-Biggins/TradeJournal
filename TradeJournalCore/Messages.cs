using Common;

namespace TradeJournalCore
{
    internal static class Messages
    {
        internal static Message ConfirmRemoveTrade => 
            new Message("Confirm", "Are you sure you want to remove this trade?", Message.MessageType.Question);
    }
}
