using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBot1.Modules.miscCore
{
    class yoMama
    {
        private static Dictionary<string, string> alerts;

        static yoMama()
        {
            string json = File.ReadAllText("Resources/yomama.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetJoke(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return "";
        }

        public static string GetFormattedJoke(string key, params object[] parameter)
        {
            if (alerts.ContainsKey(key))
            {
                return String.Format(alerts[key], parameter);
            }

            return "";
        }

        public static string GetFormattedJoke(string key, object parameter)
        {
            return GetFormattedJoke(key, new object[] { parameter });
        }
    }
}
