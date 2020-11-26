using System.Windows;
using System.Windows.Input;

namespace WxBrowser.Graphics
{

    public partial class PgTab
    {

        public PgTab()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await WebView.EnsureCoreWebView2Async();
        }

        private void GoBack(object sender, RoutedEventArgs args)
        {
            if (WebView.CanGoBack)
                WebView.GoBack();
        }

        private void GoForward(object sender, RoutedEventArgs args)
        {
            if (WebView.CanGoForward)
                WebView.GoForward();
        }

        private void RefreshPage(object sender, RoutedEventArgs args)
        {
            WebView.Reload();
        }

        private void GoHome(object sender, RoutedEventArgs args)
        {
            WebView.CoreWebView2.Navigate(App.Settings.DefaultHomePageUrl);
        }

        private void NavigateToAddress(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
                WebView.CoreWebView2?.Navigate(AddressBar.Text);
        }

    }

}