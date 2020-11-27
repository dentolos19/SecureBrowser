using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WxBrowser.Core.Bindings;

namespace WxBrowser.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WxBrowser.cfg");

        public bool ForceHttps { get; set; } = true;
        public bool PauseWebHistory { get; set; } = false;
        public string DefaultHomeAddress { get; set; } = "https://www.google.com";
        public string DefaultSearchAddress { get; set; } = "https://www.google.com/search?q={0}";
        public List<HistoryItemBinding> WebHistory { get; set; } = new List<HistoryItemBinding>();

        public void Save()
        {
            var data = JsonSerializer.Serialize(this);
            File.WriteAllText(Source, data);
        }

        public void Reset()
        {
            if (File.Exists(Source))
                File.Delete(Source);
        }

        public static Configuration Load()
        {
            if (!File.Exists(Source))
                return new Configuration();
            var data = File.ReadAllText(Source);
            return JsonSerializer.Deserialize<Configuration>(data);
        }

    }

}