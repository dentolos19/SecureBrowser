using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using WxBrowser.Core;

namespace WxBrowser.Graphics
{

    public partial class WnRuntimeInstaller
    {

        private string _downloadFilePath;
        private WebClient _client;

        public WnRuntimeInstaller()
        {
            InitializeComponent();
            var worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs args)
        {
            _downloadFilePath = Path.Combine(Path.GetTempPath(), Utilities.GenerateAlphanumeric(16) + ".exe");
            _client = new WebClient();
            _client.DownloadProgressChanged += DownloadProgressChanged;
            _client.DownloadFileCompleted += DownloadCompleted;
            _client.DownloadFileAsync(new Uri("https://go.microsoft.com/fwlink/p/?LinkId=2124703"), _downloadFilePath);
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = $"Downloading... {args.ProgressPercentage}%";
                StatusProgress.Value = args.ProgressPercentage;
            });
        }

        private void DownloadCompleted(object sender, AsyncCompletedEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = "Installing...";
                StatusProgress.IsIndeterminate = true;
            });
            var task = Process.Start(new ProcessStartInfo
            {
                FileName = _downloadFilePath,
                Arguments = "/silent /install",
                Verb = "runas"
            });
            task?.WaitForExit();
            Dispatcher.Invoke(() =>
            {
                var answer = MessageBox.Show("Installation completed! Would you like to restart this app?", "WxBrowser", MessageBoxButton.YesNo);
                if (answer == MessageBoxResult.Yes)
                {
                    Utilities.RestartApp();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            });
        }

        private void StopClosing(object sender, CancelEventArgs args)
        {
            args.Cancel = true;
            MessageBox.Show("You can't close this window while the operation is running!", "WxBrowser");
        }

    }

}