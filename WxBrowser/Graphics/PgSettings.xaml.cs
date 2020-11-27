using System.Windows;
using WxBrowser.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace WxBrowser.Graphics
{

    public partial class PgSettings
    {

        public PgSettings()
        {
            InitializeComponent();
            ForceHttpsOption.IsChecked = App.Settings.ForceHttps;
            PauseWebHistoryOption.IsChecked = App.Settings.PauseWebHistory;
        }

        private void ResetSettings(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to reset this app?", "WxBrowser", AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            App.Settings.Reset();
            Utilities.RestartApp();
        }

        private void SaveSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.ForceHttps = (bool)ForceHttpsOption.IsChecked!;
            App.Settings.PauseWebHistory = (bool)PauseWebHistoryOption.IsChecked!;
            App.Settings.Save();
            AdonisMessageBox.Show("All settings has been saved!", "WxBrowser");
        }

    }

}