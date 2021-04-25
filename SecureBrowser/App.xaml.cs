using System.Windows;
using System.Windows.Threading;
using AdonisUI;
using SecureBrowser.Core;
using SecureBrowser.Graphics;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace SecureBrowser
{

    public partial class App
    {

        public static Configuration Settings { get; } = Configuration.Load();

        private void Initialize(object sender, StartupEventArgs args)
        {
            ResourceLocator.SetColorScheme(Current.Resources, !Settings.EnableDarkMode ? ResourceLocator.LightColorScheme : ResourceLocator.DarkColorScheme);
            if (!Utilities.IsWebViewRuntimeInstalled())
            {
                if (AdonisMessageBox.Show("WebView2 Runtime is not installed! Would you like to install it automatically? Otherwise, this app will refuse to launch.", "SecureBrowser", AdonisMessageBoxButton.YesNo) == AdonisMessageBoxResult.Yes)
                {
                    Current.MainWindow = new WnRuntimeInstaller();
                }
                else
                {
                    Current.Shutdown();
                    return;
                }
            }
            else
            {
                Current.MainWindow = new WnMain();
            }
            Current.MainWindow.Show();
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnExceptionHandler(args.Exception).ShowDialog();
        }

        private void SaveSettings(object sender, ExitEventArgs args)
        {
            Settings.Save();
        }

    }

}