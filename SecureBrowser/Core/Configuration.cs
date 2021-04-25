using System;
using System.IO;
using System.Text.Json;

namespace SecureBrowser.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SecureBrowser.cfg");

        public string HomePageAddress { get; set; } = "http://google.com";
        public string DefaultSearchEndpoint { get; set; } = "http://www.google.com/search?q={0}";

        public void Save()
        {
            var data = JsonSerializer.Serialize(this);
            File.WriteAllText(Source, data);
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