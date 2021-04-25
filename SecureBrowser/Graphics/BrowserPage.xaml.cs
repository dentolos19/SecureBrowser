using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.Web.WebView2.Core;

namespace SecureBrowser.Graphics
{

    public partial class BrowserPage
    {

        private bool _isNavigating;

        public BrowserPage(string address = null)
        {
            InitializeComponent();
            InitializeWebView(address);
        }

        private async void InitializeWebView(string address = null)
        {
            await WebView.EnsureCoreWebView2Async();
            WebView.CoreWebView2.HistoryChanged += delegate
            {
                Title = WebView.CoreWebView2.DocumentTitle;
                AddressBox.Text = WebView.CoreWebView2?.Source;
            };
            WebView.CoreWebView2?.Navigate(string.IsNullOrEmpty(address) ? App.Settings.HomePageAddress : address);
        }

        private void CanGoBack(object sender, CanExecuteRoutedEventArgs args)
        {
            if (WebView?.CoreWebView2?.CanGoBack != null && !_isNavigating)
                args.CanExecute = WebView.CoreWebView2.CanGoBack;
        }

        private void GoBack(object sender, ExecutedRoutedEventArgs args)
        {
            WebView.GoBack();
        }

        private void CanGoForward(object sender, CanExecuteRoutedEventArgs args)
        {
            if (WebView?.CoreWebView2?.CanGoForward != null && !_isNavigating)
                args.CanExecute = WebView.CoreWebView2.CanGoForward;
        }

        private void GoForward(object sender, ExecutedRoutedEventArgs args)
        {
            WebView.GoForward();
        }

        private void RefreshPage(object sender, ExecutedRoutedEventArgs args)
        {
            if (_isNavigating)
            {
                WebView.CoreWebView2?.Stop();
            }
            else
            {
                WebView.CoreWebView2?.Reload();
            }
        }

        private void CanGoHome(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !_isNavigating;
        }

        private void GoHome(object sender, ExecutedRoutedEventArgs args)
        {
            WebView.CoreWebView2?.Navigate(App.Settings.HomePageAddress);
        }

        private void NavigateWebView(object sender, RoutedEventArgs args)
        {
            var address = AddressBox.Text.ToLower();
            var regex = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (regex.IsMatch(address))
            {
                if (!address.StartsWith("http://") && !address.StartsWith("https://"))
                    address = $"http://{address}";
            }
            else
            {
                address = string.Format(App.Settings.DefaultSearchEndpoint, Uri.EscapeDataString(address));
            }
            AddressBox.Text = address;
            WebView.CoreWebView2?.Navigate(address);
        }

        private void OpenSettings(object sender, RoutedEventArgs args)
        {
            if (Application.Current.MainWindow is MainWindow window)
                window.NewSettingsTab();
        }

        private void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            Title = "Navigating...";
            RefreshButton.Content = "\xE894";
            RefreshButton.ToolTip = "Stop";
            _isNavigating = true;
        }

        private void NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            RefreshButton.Content = "\xE72C";
            RefreshButton.ToolTip = "Refresh";
            _isNavigating = false;
        }

    }

}