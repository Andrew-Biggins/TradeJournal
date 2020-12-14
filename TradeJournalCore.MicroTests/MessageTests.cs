using Common;
using Common.MicroTests;
using System;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class MessageTests
    {
        [Gwt("Given null name",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>("nameKey", () => new Message(null!, string.Empty, Message.MessageType.Error));
        }

        [Gwt("Given null content",
            "when created",
            "an exception is thrown")]
        public void T1()
        {
            Assert.Throws<ArgumentNullException>("contentKey", () => new Message(string.Empty, null!, Message.MessageType.Error));
        }

        [Gwt("Given a message is created with a type",
            "when the message type is read",
            "it is the type given at creation")]
        public void T2()
        {
            // Arrange
            var message = new Message(string.Empty, string.Empty, Message.MessageType.Question);

            // Act
            var actual = message.Type;

            // Assert
            Assert.Equal(Message.MessageType.Question, actual);
        }

        [Gwt("Given a message is created with content",
            "when the content is read",
            "it is the content given at creation")]
        public void T3()
        {
            // Arrange
            var message = new Message(string.Empty, nameof(T3), Message.MessageType.Error);

            // Act
            var actual = message.ContentKey;

            // Assert
            Assert.Equal(nameof(T3), actual);
        }

        [Gwt("Given a message is created with a name",
            "when the name is read",
            "it is the name given at creation")]
        public void T4()
        {
            // Arrange
            var message = new Message(nameof(T4), string.Empty, Message.MessageType.Error);

            // Act
            var actual = message.NameKey;

            // Assert
            Assert.Equal(nameof(T4), actual);
        }
    }
}
