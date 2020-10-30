using NSubstitute;
using System;
using System.ComponentModel;

namespace Common.MicroTests
{
    public sealed class CatcherTests
    {
        [Gwt("Given a catcher is listening to a property changed event when it is raised",
            "when the catcher is checked with the correct name",
            "the property changed with the same name has been caught")]
        public void T0()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Substitute.For<ICatcher<PropertyChangedEventArgs>>();
            raiser.PropertyChanged += catcher.Catch;

            // Act 
            raiser.RaisePropertyChanged(TestPropertyName);

            // Assert
            catcher.CaughtPropertyChanged(raiser, TestPropertyName);
        }

        [Gwt("Given a catcher is listening to a property changed event when it is raised",
            "when the catcher is checked with an incorrect name",
            "the property changed with the correct name has not been caught")]
        public void T1()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Substitute.For<ICatcher<PropertyChangedEventArgs>>();
            raiser.PropertyChanged += catcher.Catch;

            // Act 
            raiser.RaisePropertyChanged(TestPropertyName);

            // Assert
            catcher.DidNotCatchPropertyChanged(raiser, "incorrect");
        }

        [Gwt("Given a catcher is listening to an INotifyPropertyChanged when property changed is raised",
            "when the catcher is checked with the correct name",
            "the property changed with the same name has been caught")]
        public void T2()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Catcher.For(raiser);

            // Act 
            raiser.RaisePropertyChanged(TestPropertyName);

            // Assert
            catcher.CaughtPropertyChanged(raiser, TestPropertyName);
        }

        [Gwt("Given a catcher is listening for an event when it is raised",
            "when the catcher is checked",
            "the property changed with the same name has been caught")]
        public void T3()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Substitute.For<ICatcher<EventArgs>>();
            raiser.Empty += catcher.Catch;

            // Act
            raiser.Empty += Raise.Event();

            // Assert
            catcher.CaughtEmpty(raiser);
        }

        [Gwt("Given a catcher is listening for an event",
            "when a different event is raised",
            "the catcher did not catch anything")]
        public void T4()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Catcher.Simple;
            raiser.PropertyChanged += catcher.Catch;

            // Act
            raiser.Empty += Raise.Event();

            // Assert
            catcher.DidNotCatch();
        }

        [Gwt("Given a catcher is listening for an event",
            "when a different event is raised",
            "the catcher did not catch given specific event arguments")]
        public void T5()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Catcher.For(raiser);

            // Act
            raiser.RaisePropertyChanged("incorrect");

            // Assert
            catcher.DidNotCatch(raiser, new PropertyChangedEventArgs("correct"));
        }

        [Gwt("Given a catcher is listening for an event",
            "when a different event is raised",
            "the catcher did not catch an event with the correct property name")]
        public void T6()
        {
            // Arrange
            var raiser = SubRaiser;
            var catcher = Catcher.For(raiser);

            // Act
            raiser.RaisePropertyChanged("incorrect");

            // Assert
            catcher.DidNotCatch(raiser, new PropertyChangedEventArgs("correct"));
        }

        private static IEventRaiser SubRaiser => Substitute.For<IEventRaiser>();

        private const string TestPropertyName = "TestProperty";
    }
}
