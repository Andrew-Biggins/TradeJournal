using System;
using System.ComponentModel;

namespace Common.MicroTests
{
    public interface IEventRaiser : INotifyPropertyChanged
    {
        event EventHandler? Empty;
    }
}