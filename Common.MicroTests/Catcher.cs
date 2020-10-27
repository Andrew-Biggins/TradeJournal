using System;
using System.ComponentModel;
using NSubstitute;

namespace Common.MicroTests
{
    public static class Catcher
    {
        public static ICatcher<PropertyChangedEventArgs> PropertyChanged =>
            Substitute.For<ICatcher<PropertyChangedEventArgs>>();

        public static ICatcher<EventArgs> Simple => Substitute.For<ICatcher<EventArgs>>();

        public static ICatcher<T> For<T>() => Substitute.For<ICatcher<T>>();

        public static ICatcher<PropertyChangedEventArgs> For(INotifyPropertyChanged sender)
        {
            var catcher = PropertyChanged;
            sender.PropertyChanged += catcher.Catch;
            return catcher;
        }
    }
}
