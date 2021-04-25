using System.Windows;
using SecureBrowser.Core;
using SecureBrowser.Graphics;

namespace SecureBrowser
{

    public partial class App
    {

        internal static Configuration Settings { get; } = Configuration.Load();

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

    }

}