using System.Windows.Controls;

namespace WxBrowser.Core.Bindings
{

    public class ViewTabBinding
    {

        public ViewTabBinding(Page page)
        {
            Content = page;
        }

        public Page Content { get; }

    }

}