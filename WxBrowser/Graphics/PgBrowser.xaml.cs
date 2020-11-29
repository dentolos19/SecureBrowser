using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Web.WebView2.Core;
using WxBrowser.Core.Bindings;

namespace WxBrowser.Graphics
{

    public partial class PgBrowser
    {

        private bool _isNavigating;

        public PgBrowser()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await WebView.EnsureCoreWebView2Async();
            if (WebView.CoreWebView2 == null)
                return;
            WebView.CoreWebView2.HistoryChanged += delegate
            {
                AddressBar.Text = WebView.Source.ToString();
                if (!App.Settings.PauseWebHistory)
                    App.Settings.WebHistory.Add(new HistoryItemBinding { Title = WebView.CoreWebView2.DocumentTitle, Address = WebView.Source.ToString(), Time = DateTime.Now });
            };
            WebView.CoreWebView2.Navigate(App.Settings.DefaultHomeAddress);
        }

        private void GoBack(object sender, ExecutedRoutedEventArgs args)
        {
            if (WebView.CanGoBack)
                WebView.GoBack();
        }

        private void CanGoBack(object sender, CanExecuteRoutedEventArgs args)
        {
            if (WebView?.CoreWebView2?.CanGoBack != null && !_isNavigating)
                args.CanExecute = WebView.CoreWebView2.CanGoBack;
        }

        private void GoForward(object sender, ExecutedRoutedEventArgs args)
        {
            if (WebView.CanGoForward)
                WebView.GoForward();
        }

        private void CanGoForward(object sender, CanExecuteRoutedEventArgs args)
        {
            if (WebView?.CoreWebView2?.CanGoForward != null && !_isNavigating)
                args.CanExecute = WebView.CoreWebView2.CanGoForward;
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

        private void GoHome(object sender, ExecutedRoutedEventArgs args)
        {
            WebView.CoreWebView2?.Navigate(App.Settings.DefaultHomeAddress);
        }

        private void CanGoHome(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = !_isNavigating;
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
                address = string.Format(App.Settings.DefaultSearchAddress, Uri.EscapeDataString(address));
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
            RefreshButton.Content = "\xE894";
            RefreshButton.ToolTip = "Stop";
            _isNavigating = true;
        }

        private void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs args)
        {
            Title = WebView.CoreWebView2.DocumentTitle;
            RefreshButton.Content = "\xE72C";
            RefreshButton.ToolTip = "Refresh";
            _isNavigating = false;
        }

        private void NewTab(object sender, RoutedEventArgs args)
        {
            if (Application.Current.MainWindow is WnMain window)
                window.NewBrowserTab();
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