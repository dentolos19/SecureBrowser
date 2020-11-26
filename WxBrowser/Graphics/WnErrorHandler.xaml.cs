using System;
using System.Windows;
using WxBrowser.Core;

namespace WxBrowser.Graphics
{

    public partial class WnErrorHandler
    {

        public WnErrorHandler(Exception error)
        {
            InitializeComponent();
            MessageText.Text = error.Message;
            StackTraceText.Text = error.StackTrace!;
        }

        private void Restart(object sender, RoutedEventArgs args)
        {
            Utilities.RestartApp();
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

    }

}