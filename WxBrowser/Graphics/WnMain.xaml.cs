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
            _tabs.Add(new ViewTabBinding(new PgBrowser()));
        }

        private void CloseViewTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.Remove(_tabs[ViewControl.SelectedIndex]);
        }

    }

}