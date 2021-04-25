using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using SecureBrowser.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SecureBrowser.Graphics
{

    public partial class WnRuntimeInstaller
    {

        private readonly WebClient _client = new();
        private readonly string _downloadFilePath = Path.Combine(Path.GetTempPath(), Utilities.GenerateAlphanumeric(16) + ".exe");

        public WnRuntimeInstaller()
        {
            InitializeComponent();
            var worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.RunWorkerAsync();
        }

        private void DoWork(object? sender, DoWorkEventArgs args)
        {
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

        private void DownloadCompleted(object? sender, AsyncCompletedEventArgs args)
        {
            if (args.Cancelled)
            {
                if (args.Error != null)
                    throw args.Error;
                AdonisMessageBox.Show("The download operation was cancelled! Unable to install runtime!", "SecureBrowser");
                if (File.Exists(_downloadFilePath))
                    File.Delete(_downloadFilePath);
                Dispatcher.Invoke(() => { Application.Current.Shutdown(); });
            }
            else
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
                    Verb = "runas",
                    UseShellExecute = true
                });
                task?.WaitForExit();
                File.Delete(_downloadFilePath);
                Dispatcher.Invoke(() => { Utilities.RestartApp(); });
            }
        }

        private void StopClosing(object sender, CancelEventArgs args)
        {
            args.Cancel = true;
            AdonisMessageBox.Show("You can't close this window while the operation is running!", "SecureBrowser");
        }

    }

}