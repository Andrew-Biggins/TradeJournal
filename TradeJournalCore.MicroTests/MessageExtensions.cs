using Common;

namespace TradeJournalCore.MicroTests
{
    internal static class MessageExtensions
    {
        internal static bool Is(this Message message, Message other)
        {
            return message.ContentKey == other.ContentKey &&
                   message.NameKey == other.NameKey &&
                   message.Type == other.Type;
        }
    }
}
