using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DiscordBot1
{
    class Config
    {
        private const string configFolder = "Resources"; 
        private const string configFile = "config.json";

        public static BotConfig bot;
        

        static Config()
        {
            //Checks to see if config folder exists and if not it will create it.
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            //Checks to see if the config json file exists. If it does not exist, it will create it with the specifications of BotConfig
            if(!File.Exists(configFolder + "/" + configFile))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            //The file exists, so it reads the file
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }


    //define structure of BotConfig
    public struct BotConfig
    {
        public string token; 
        public string cmdPrefix; 
    }
}
