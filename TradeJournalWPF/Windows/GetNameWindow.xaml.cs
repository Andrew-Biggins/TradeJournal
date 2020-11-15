using System.Windows;
using System.Windows.Input;
using Common;
using TradeJournalCore.ViewModels;

namespace TradeJournalWPF.Windows
{
    public partial class GetNameWindow : Window
    {
        public GetNameWindow()
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
                var vm = (GetNameViewModel)DataContext;
                vm.ConfirmNewNameCommand.Execute(null!);
                Close();
            }
        }
    }
}
