using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace WxBrowser.Core
{

    public static class Utilities
    {

        public static void RestartApp(string args = null)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            if (location.EndsWith(".dll", StringComparison.CurrentCultureIgnoreCase))
                location = Path.Combine(Path.GetDirectoryName(location)!, Path.GetFileNameWithoutExtension(location) + ".exe");
            Process.Start(location, args ?? string.Empty);
            Application.Current.Shutdown();
        }

        public static string GenerateAlphanumeric(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length).Select(index => index[random.Next(index.Length)]).ToArray());
        }

        public static bool IsWebViewRuntimeInstalled()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}");
                if (key == null)
                    return false;
                var version = (string)key.GetValue("pv");
                return !string.IsNullOrEmpty(version);
            }
            else
            {
                var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}");
                if (key == null)
                    return false;
                var version = (string)key.GetValue("pv");
                return !string.IsNullOrEmpty(version);
            }
        }

    }

}