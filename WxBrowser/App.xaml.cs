using System.Windows;
using System.Windows.Threading;
using WxBrowser.Core;
using WxBrowser.Graphics;

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
                Current.MainWindow = new WnRuntimeInstaller();
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

    }

}