using System.Windows;
using WxBrowser.Graphics;

namespace WxBrowser
{

    public partial class App
    {

        private void Initialize(object sender, StartupEventArgs args)
        {
            Current.MainWindow = new WnMain();
            Current.MainWindow.Show();
        }

    }

}