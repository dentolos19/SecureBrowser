using System.Windows;
using System.Windows.Threading;
using WxBrowser.Core;
using WxBrowser.Graphics;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace WxBrowser
{

    public partial class App
    {
        public static Configuration Settings { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            if (!Utilities.IsWebViewRuntimeInstalled())
            {
                if (AdonisMessageBox.Show("WebView Runtime is not installed! Would you like to install it automatically? Otherwise, this app will refuse to launch.", "WxBrowser", AdonisMessageBoxButton.YesNo) == AdonisMessageBoxResult.Yes)
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

        private void HandleError(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnErrorHandler(args.Exception).ShowDialog();
        }

        private void SaveSettings(object sender, ExitEventArgs args)
        {
            Settings.Save();
        }

    }

}