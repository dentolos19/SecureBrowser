using System;
using System.IO;
using System.Xml.Serialization;

namespace WxBrowser.Core
{

    public class Configuration
    {

        private static readonly string Source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WxBrowser.cfg");
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Configuration));

        public bool ForceHttps { get; set; } = true;
        public string DefaultPageUrl { get; set; } = "https://www.google.com";
        public string DefaultSearchUrl { get; set; } = "https://www.google.com/search?q={0}";

        public void Save()
        {
            using var stream = new FileStream(Source, FileMode.Create);
            Serializer.Serialize(stream, this);
        }

        public static Configuration Load()
        {
            if (!File.Exists(Source))
                return new Configuration();
            using var stream = new FileStream(Source, FileMode.Open);
            return (Configuration)Serializer.Deserialize(stream);
        }

    }

}