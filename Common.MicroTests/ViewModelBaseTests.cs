using Xunit;

namespace Common.MicroTests
{
    public sealed class ViewModelBaseTests
    {
        [Gwt("Given a property changed is subscribed to",
            "when a property is set",
            "property changed is raised with the name of the property")]
        public void T0()
        {
            // Arrange
            var derived = new Derived();
            var catcher = Catcher.PropertyChanged;
            derived.PropertyChanged += catcher.Catch;

            // Act
            derived.Value = 12;

            // Assert
            catcher.CaughtPropertyChanged(derived, nameof(Derived.Value));
        }

        [Gwt("Given a view model base",
            "when a property changed is explicitly requested",
            "property changed is raised")]
        public void T1()
        {
            // Arrange
            var derived = new Derived();
            var catcher = Catcher.PropertyChanged;
            derived.PropertyChanged += catcher.Catch;

            // Act
            derived.RaisePropertyChanged(nameof(Derived.Value));

            // Assert
            catcher.CaughtPropertyChanged(derived, nameof(Derived.Value));
        }

        [Gwt("Given a property is set",
            "when the property value is obtained",
            "the property has the value that was set")]
        public void T2()
        {
            // Arrange
            var derived = new Derived();

            // Act
            derived.Value = 12;

            // Assert
            Assert.Equal(12, derived.Value);
        }

        private sealed class Derived : ViewModelBase
        {
            internal int Value
            {
                get => _value;
                set => SetProperty(ref _value, value, nameof(Value));
            }

            internal new void RaisePropertyChanged(string name)
            {
                base.RaisePropertyChanged(name);
            }

            private int _value;
        }
    }
}
