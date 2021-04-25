using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using SecureBrowser.Core.Bindings;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace SecureBrowser.Graphics
{

    public partial class PgHistory
    {

        public PgHistory()
        {
            InitializeComponent();
            RefreshHistory(null!, null!);
        }

        private bool Filter(object item)
        {
            var input = FilterBox.Text;
            if (string.IsNullOrEmpty(input))
                return true;
            return ((HistoryItemBinding)item).Title.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void RefreshHistory(object sender, RoutedEventArgs args)
        {
            var list = new List<HistoryItemBinding>();
            for (var index = App.Settings.WebHistory.Count - 1; !(index <= 0); index--)
                list.Add(App.Settings.WebHistory[index]);
            HistoryList.ItemsSource = list;
            var view = (CollectionView)CollectionViewSource.GetDefaultView(HistoryList.ItemsSource);
            if (view != null)
                view.Filter += Filter;
        }

        private void ClearHistory(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to clear your web history?", "SecureBrowser", AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            HistoryList.ItemsSource = null;
            App.Settings.WebHistory.Clear();
        }

        private void FilterHistory(object sender, TextChangedEventArgs args)
        {
            CollectionViewSource.GetDefaultView(HistoryList.ItemsSource)?.Refresh();
        }

        private void OpenHistory(object sender, MouseButtonEventArgs args)
        {
            if (HistoryList.SelectedItem is HistoryItemBinding binding && Application.Current.MainWindow is WnMain window)
                window.NewBrowserTab(binding.Address);
        }

        private void CopyAddress(object sender, RoutedEventArgs args)
        {
            if (HistoryList.SelectedItem is HistoryItemBinding binding)
                Clipboard.SetText(binding.Address);
        }

    }

}