using System;
using NSubstitute;
using System.ComponentModel;

namespace Common.MicroTests
{
    public static class CatcherExtensions
    {
        public static void CaughtPropertyChanged(this ICatcher<PropertyChangedEventArgs> subCatcher, object sender, string propertyName)
        {
            subCatcher.Received(1).Catch(sender, Arg.Is<PropertyChangedEventArgs>(p => p.PropertyName == propertyName));
        }

        public static void DidNotCatchPropertyChanged(this ICatcher<PropertyChangedEventArgs> subCatcher, object sender, string propertyName)
        {
            subCatcher.DidNotReceive().Catch(sender, Arg.Is<PropertyChangedEventArgs>(e => e != null &&
                                                                                           e.PropertyName == propertyName));
        }

        public static void CaughtEmpty(this ICatcher<EventArgs> subCatcher, object sender)
        {
            subCatcher.Received(1).Catch(sender, EventArgs.Empty);
        }

        public static void DidNotCatch<T>(this ICatcher<T> subCatcher)
        {
            subCatcher.DidNotReceiveWithAnyArgs().Catch(null, default!);
        }

        public static void DidNotCatch<T>(this ICatcher<T> subCatcher, object sender, T args)
        {
            subCatcher.DidNotReceive().Catch(sender, Arg.Is<T>(e => e != null &&
                                                                    e.Equals(args)));
        }
    }
}
