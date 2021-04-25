using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SecureBrowser.Core.Bindings;

namespace SecureBrowser.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SecureBrowser.cfg");

        public bool ForceHttps { get; set; } = true;
        public bool PauseHistory { get; set; }
        public bool EnableDarkMode { get; set; } = true;
        public string DefaultHomeAddress { get; set; } = "https://duckduckgo.com";
        public string DefaultSearchAddress { get; set; } = "https://duckduckgo.com/?q={0}";
        public List<HistoryItemBinding> WebHistory { get; set; } = new();

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
            return JsonSerializer.Deserialize<Configuration>(data)!;
        }

    }

}