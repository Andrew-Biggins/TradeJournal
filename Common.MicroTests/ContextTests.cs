using System;
using System.Threading;
using Xunit;

namespace Common.MicroTests
{
    public sealed class ContextTests
    {
        [Gwt("Given a null context",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>(() => new Context(null!));
        }

        [Gwt("Given a null context",
            "when callback is sent",
            "the callback is executed")]
        public void T1()
        {
            // Arrange
            var realContext = new SynchronizationContext();
            var context = new Context(realContext);
            var result = false;

            // Act
            context.Send(_ => result = true);

            // Assert
            Assert.True(result);
        }
    }
}
