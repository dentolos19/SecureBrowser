using System.Windows;
using System.Windows.Input;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace WxBrowser.Graphics
{

    public partial class PgHistory
    {

        public PgHistory()
        {
            InitializeComponent();
            RefreshHistory(null, null);
        }

        private void RefreshHistory(object sender, RoutedEventArgs args)
        {
            HistoryList.Items.Clear();
            for (var index = App.Settings.WebHistory.Count - 1; !(index <= 0); index--)
                HistoryList.Items.Add(App.Settings.WebHistory[index]);
        }

        private void ClearHistory(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to clear your web history?", "WxBrowser", AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            HistoryList.Items.Clear();
            App.Settings.WebHistory.Clear();
        }

        private void FilterHistory(object sender, KeyEventArgs args)
        {
            // TODO
        }

    }

}