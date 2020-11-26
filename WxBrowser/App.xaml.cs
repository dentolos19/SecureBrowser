using System.Windows;
using System.Windows.Threading;
using WxBrowser.Core;
using WxBrowser.Graphics;

namespace WxBrowser
{

    public partial class App
    {

        public static bool IsWebViewRuntimeInstalled { get; private set; }
        public static Configuration Settings { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            IsWebViewRuntimeInstalled = Utilities.IsWebViewRuntimeInstalled();
            if (!IsWebViewRuntimeInstalled)
            {
                var answer = MessageBox.Show("WebView2 Runtime is not installed your machine! Would you like to automatically install it on your machine?", "WxBrowser", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Current.MainWindow = new WnRuntimeInstaller();
                    Current.MainWindow.Show();
                    return;
                }
            }
            Current.MainWindow = new WnMain();
            Current.MainWindow.Show();
        }

        private void HandleError(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnErrorHandler(args.Exception).ShowDialog();
        }

    }

}