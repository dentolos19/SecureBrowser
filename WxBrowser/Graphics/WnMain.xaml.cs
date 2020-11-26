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
            ViewTabs.ItemsSource = _tabs;
            _tabs.Add(new ViewTabBinding(new PgTab()));
        }

        private void CloseViewTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.Remove(_tabs[ViewTabs.SelectedIndex]);
        }

    }

}