using System;
using Common.MicroTests;
using Common.Optional;
using Xunit;

namespace TradeJournalCore.MicroTests
{
    public sealed class ExcursionTests
    {
        [Gwt("Given an excursion with no arguments",
            "when the excursion points is read",
            "the excursion points is none by default")]
        public void T0()
        {
            // Arrange
            var excursion = new Excursion();

            // Act 
            var actual = excursion.Points;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an excursion with no arguments",
            "when the excursion percentage is read",
            "the excursion percentage is none by default")]
        public void T1()
        {
            // Arrange
            var excursion = new Excursion();

            // Act 
            var actual = excursion.Percentage;

            // Assert
            Assert.IsType<Option.OptionNone<double>>(actual);
        }

        [Gwt("Given an excursion with some arguments",
            "when the excursion points is read",
            "the excursion points is the same as the one given at construction")]
        public void T2()
        {
            // Arrange
            const double testValue = 123.45;
            var excursion = new Excursion(testValue, 0);
            var outValue = 1.00;

            // Act 
            excursion.Points.IfExistsThen(x => { outValue = x; });

            // Assert
            Assert.Equal(testValue, outValue);
        }

        [Gwt("Given an excursion with some arguments",
            "when the excursion percentage is read",
            "the excursion percentage is the same as the one given at construction")]
        public void T3()
        {
            // Arrange
            const double testValue = 476.77;
            var excursion = new Excursion(0, testValue);
            var outValue = 1.00;

            // Act 
            excursion.Percentage.IfExistsThen(x => { outValue = x; });

            // Assert
            Assert.Equal(testValue, outValue);
        }
    }
}
