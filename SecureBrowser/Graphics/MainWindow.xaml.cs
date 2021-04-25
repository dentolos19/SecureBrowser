using System;
using System.Collections.ObjectModel;
using SecureBrowser.Core.Bindings;
using Syncfusion.Windows.Tools.Controls;

namespace SecureBrowser.Graphics
{

    public partial class MainWindow
    {

        private readonly ObservableCollection<TabBinding> _tabs = new();

        public MainWindow()
        {
            InitializeComponent();
            Tabs.ItemsSource = _tabs;
            NewBrowserTab();
        }

        public void NewBrowserTab(string address = null) => _tabs.Add(new TabBinding { Content = new BrowserPage(address) });
        public void NewScraperTab(string address) => _tabs.Add(new TabBinding { Content = new ScraperPage(address) });
        public void NewSettingsTab() => _tabs.Add(new TabBinding { Content = new SettingsPage() });

        private void AddTab(object sender, EventArgs args)
        {
            NewBrowserTab();
        }

        private void RemoveTab(object sender, CloseTabEventArgs args)
        {
            if (args.TargetTabItem.DataContext is TabBinding binding)
                _tabs.Remove(binding);
        }

    }

}