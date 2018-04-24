using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot1.Core.Store
{
    public static class GBPstores
    {
        private static List<GBPstore> storeAccounts;

        private static string accountsFile = "Resources/GBPstore.json";

        static GBPstores()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                storeAccounts = DataStorage.LoadGBPstoreAccounts(accountsFile).ToList();
            }
            else
            {
                storeAccounts = new List<GBPstore>();
                SaveGBPStoreAccounts();
            }
        }

        public static void SaveGBPStoreAccounts()
        {
            DataStorage.SaveGBPstoreAccounts(storeAccounts, accountsFile);
        }

        public static GBPstore GetAccount(string type)
        {
            return GetOrCreateGBPStore(type);
        }

        private static GBPstore GetOrCreateGBPStore(string type)
        {
            var result = from a in storeAccounts
                         where a.inventory == type
                         select a;

            var account = result.FirstOrDefault();
            if (account == null) account = CreateGBPStore(type);
            return account;
        }

        private static GBPstore CreateGBPStore(string type)
        {
            var newAccount = new GBPstore()
            {
                account = type,
                inventory = "",
                time = new DateTime(),
                list = {}
            };

            storeAccounts.Add(newAccount);
            SaveGBPStoreAccounts();
            return newAccount;
        }
    }
}
