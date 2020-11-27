using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.Web.WebView2.Core;

namespace WxBrowser.Graphics
{

    public partial class PgBrowser
    {

        private bool _isNavigating;

        public PgBrowser()
        {
            InitializeComponent();
            if (App.IsWebViewRuntimeInstalled)
            {
                InitializeWebView();
                ViewControl.SelectedIndex = 0;
            }
            else
            {
                ViewControl.SelectedIndex = 1;
            }
        }

        private async void InitializeWebView()
        {
            await WebView.EnsureCoreWebView2Async();
            WebView.CoreWebView2?.Navigate(App.Settings.DefaultPageUrl);
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
            if (_isNavigating)
            {
                WebView.CoreWebView2?.Stop();
            }
            else
            {
                WebView.CoreWebView2?.Reload();
            }
        }

        private void GoHome(object sender, RoutedEventArgs args)
        {
            WebView.CoreWebView2?.Navigate(App.Settings.DefaultPageUrl);
        }

        private void NavigateToAddress(object sender, KeyEventArgs args)
        {
            if (args.Key != Key.Enter)
                return;
            var address = AddressBar.Text.ToLower();
            var regex = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (regex.IsMatch(address))
            {
                if (!address.StartsWith("http://") && !address.StartsWith("https://"))
                {
                    address = $"http://{address}";
                }
            }
            else
            {
                address = string.Format(App.Settings.DefaultSearchUrl, Uri.EscapeDataString(address));
            }
            if (App.Settings.ForceHttps)
            {
                if (address.StartsWith("http://"))
                {
                    address = $"https://{address[7..]}";
                }
            }
            AddressBar.Text = address;
            WebView.CoreWebView2?.Navigate(address);
        }

        private void NavigatingAddress(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            Title = "Navigating...";
            BackButton.IsEnabled = false;
            ForwardButton.IsEnabled = false;
            RefreshButton.Content = "\xE894";
            RefreshButton.ToolTip = "Stop";
            HomeButton.IsEnabled = false;
            _isNavigating = true;
        }

        private void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            Title = WebView.CoreWebView2.DocumentTitle;
            AddressBar.Text = WebView.Source.ToString();
            BackButton.IsEnabled = WebView.CanGoBack;
            ForwardButton.IsEnabled = WebView.CanGoForward;
            RefreshButton.Content = "\xE72C";
            RefreshButton.ToolTip = "Refresh";
            HomeButton.IsEnabled = true;
            _isNavigating = false;
        }

        private void ShowHistory(object sender, RoutedEventArgs args)
        {
            if (Application.Current.MainWindow is WnMain window)
                window.NewHistoryTab();
        }

        private void ShowDownloads(object sender, RoutedEventArgs args)
        {
            if (Application.Current.MainWindow is WnMain window)
                window.NewDownloadsTab();
        }

        private void ShowSettings(object sender, RoutedEventArgs args)
        {
            if (Application.Current.MainWindow is WnMain window)
                window.NewSettingsTab();
        }

    }

}