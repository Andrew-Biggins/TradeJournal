using NSubstitute;
using System.Collections;
using System.ComponentModel;

namespace Common.MicroTests
{
    public static class Sub
    {
        public static void RaisePropertyChanged(this INotifyPropertyChanged sub, string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            sub.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(e);
        }

        public static void ClearReceivedCalls(IEnumerable subs)
        {
            foreach (var sub in subs)
            {
                sub.ClearReceivedCalls();
            }
        }
    }
}
