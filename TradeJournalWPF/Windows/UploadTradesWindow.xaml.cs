using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TradeJournalCore.ViewModels;

namespace TradeJournalWPF.Windows
{
    /// <summary>
    /// Interaction logic for UploadTradesWindow.xaml
    /// </summary>
    public partial class UploadTradesWindow : Window
    {
        public UploadTradesWindow()
        {
            InitializeComponent();
        }

        private void OnSelectFileButtonClick(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "CSV Files|*.csv",
                DefaultExt = "*.csv"
            };

            if (fileDialog.ShowDialog() == true) FilePathTextBox.Text = fileDialog.FileName;
        }

        private void OnFilePathTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void OnConfirmFilePathButtonClick(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            vm.StartTradeUploadCommand.Execute(null);
            Close();
        }
    }
}
