using System.Collections.ObjectModel;
using System.Windows.Input;
using WxBrowser.Core.Bindings;

namespace WxBrowser.Graphics
{

    public partial class WnMain
    {

        private readonly ObservableCollection<ViewTabBinding> _tabs = new ObservableCollection<ViewTabBinding>();

        public WnMain()
        {
            InitializeComponent();
            ViewControl.ItemsSource = _tabs;
            NewBrowserTab();
        }

        public void NewBrowserTab()
        {
            var tab = new ViewTabBinding { Content = new PgBrowser() };
            _tabs.Add(tab);
            ViewControl.SelectedIndex = _tabs.IndexOf(tab);
        }

        public void NewHistoryTab()
        {
            var tab = new ViewTabBinding { Content = new PgHistory() };
            _tabs.Add(tab);
            ViewControl.SelectedIndex = _tabs.IndexOf(tab);
        }

        public void NewDownloadsTab()
        {
            var tab = new ViewTabBinding { Content = new PgDownloads() };
            _tabs.Add(tab);
            ViewControl.SelectedIndex = _tabs.IndexOf(tab);
        }

        public void NewSettingsTab()
        {
            var tab = new ViewTabBinding { Content = new PgSettings() };
            _tabs.Add(tab);
            ViewControl.SelectedIndex = _tabs.IndexOf(tab);
        }

        private void CloseViewTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.Remove(_tabs[ViewControl.SelectedIndex]);
        }

    }

}