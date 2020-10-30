using System.ComponentModel;

namespace Common
{
#nullable enable
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetProperty<T>(ref T member, T value, string propertyName = "")
        {
            if (!Equals(member, value))
            {
                member = value;
                PropertyChanged.Raise(this, propertyName);
            }
        }

        protected void RaisePropertyChanged(string propertyName) => PropertyChanged.Raise(this, propertyName);
    }
#nullable disable
}
