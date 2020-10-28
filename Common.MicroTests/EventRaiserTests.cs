using System;
using NSubstitute;
using System.ComponentModel;

namespace Common.MicroTests
{
    public sealed class EventRaiserTests
    {
        [Gwt("Given a property changed event has a handler",
            "when the event is raised",
            "the handler is invoked")]
        public void T0()
        {
            // Arrange
            var catcher = Catcher.PropertyChanged;
            PropertyChangedEvent += catcher.Catch;
            const string propertyName = nameof(propertyName);

            // Act
            PropertyChangedEvent.Raise(this, propertyName);

            // Assert
            catcher.CaughtPropertyChanged(this, propertyName);
        }

        [Gwt("Given an event has a handler",
            "when the event is raised",
            "the handler is invoked")]
        public void T1()
        {
            // Arrange
            var catcher = Catcher.Simple;
            Event += catcher.Catch;

            // Act
            Event.Raise(this);

            // Assert
            catcher.CaughtEmpty(this);
        }

        private event EventHandler? Event;
        private event PropertyChangedEventHandler? PropertyChangedEvent;
    }
}
