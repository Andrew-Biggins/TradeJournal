using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TradeJournalCore.ViewModels;

namespace TradeJournalWPF.Windows
{
    public partial class AddMarketWindow : Window
    {
        public AddMarketWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TextBox.Text != string.Empty)
            {
                var vm = (AddMarketViewModel)DataContext;
                vm.ConfirmMarketCommand.Execute(null!);
                Close();
            }
        }
    }
}
