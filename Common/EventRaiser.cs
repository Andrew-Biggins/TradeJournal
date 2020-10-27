using System;
using System.ComponentModel;

namespace Common
{
#nullable enable
    public static class EventRaiser
    {
        public static void Raise(this EventHandler? handler, object sender)
        {
            handler?.Invoke(sender, EventArgs.Empty);
        }

        public static void Raise(this PropertyChangedEventHandler? handler, object sender, string propertyName)
        {
            handler?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
#nullable disable
}
