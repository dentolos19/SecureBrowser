using System;

namespace WxBrowser.Core.Bindings
{

    public class HistoryItemBinding
    {

        public HistoryItemBinding(string title, string url, DateTime time)
        {
            Title = title;
            Url = url;
            Time = time;
        }

        public string Title { get; }
        public string Url { get; }
        public DateTime Time { get; }

    }

}