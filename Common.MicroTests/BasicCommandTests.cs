using System;
using Xunit;

namespace Common.MicroTests
{
    public sealed class BasicCommandTests
    {
        [Gwt("Given a null action",
            "when created",
            "an exception is thrown")]
        public void T0()
        {
            Assert.Throws<ArgumentNullException>(() => new BasicCommand(null!));
        }

        [Gwt("Given a command",
            "when checked to see if it can execute with a null",
            "true is returned")]
        public void T1()
        {
            // Arrange
            var command = new BasicCommand(() => { });

            // Act
            var canExecute = command.CanExecute(null!);

            // Assert
            Assert.True(canExecute);
        }

        [Gwt("Given a command",
            "when executed",
            "the action is invoked")]
        public void T2()
        {
            // Arrange
            var invoked = false;
            var command = new BasicCommand(() => invoked = true);

            // Act
            command.Execute(new object());

            // Assert
            Assert.True(invoked);
        }
    }
}
