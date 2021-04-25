using System.Windows;
using AdonisUI;
using SecureBrowser.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace SecureBrowser.Graphics
{

    public partial class PgSettings
    {

        public PgSettings()
        {
            InitializeComponent();
            ForceHttpsOption.IsChecked = App.Settings.ForceHttps;
            PauseWebHistoryOption.IsChecked = App.Settings.PauseHistory;
            EnableDarkModeOption.IsChecked = App.Settings.EnableDarkMode;
        }

        private void ResetSettings(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to reset this app?", "SecureBrowser", AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            App.Settings.Reset();
            Utilities.RestartApp();
        }

        private void SaveSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.ForceHttps = (bool)ForceHttpsOption.IsChecked!;
            App.Settings.PauseHistory = (bool)PauseWebHistoryOption.IsChecked!;
            App.Settings.EnableDarkMode = (bool)EnableDarkModeOption.IsChecked!;
            App.Settings.Save();
            ResourceLocator.SetColorScheme(Application.Current.Resources, !App.Settings.EnableDarkMode ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
            AdonisMessageBox.Show("All settings has been saved!", "SecureBrowser");
        }

    }

}