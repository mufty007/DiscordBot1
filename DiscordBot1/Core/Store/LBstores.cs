using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot1.Core.Store
{
    public static class LBstores
    {
        private static List<LBstore> storeAccounts;

        private static string accountsFile = "Resources/LBstore.json";

        static LBstores()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                storeAccounts = DataStorage.LoadLBstoreAccounts(accountsFile).ToList();
            }
            else
            {
                storeAccounts = new List<LBstore>();
                SaveLBStoreAccounts();
            }
        }

        public static void SaveLBStoreAccounts()
        {
            DataStorage.SaveLBstoreAccounts(storeAccounts, accountsFile);
        }

        public static LBstore GetAccount(string type)
        {
            return GetOrCreateLBStoreAccount(type);
        }

        private static LBstore GetOrCreateLBStoreAccount(string type)
        {
            var result = from a in storeAccounts
                         where a.account == type
                         select a;

            var account = result.FirstOrDefault();
            if (account == null) account = CreateLBStoreAccount(type);
            return account;
        }

        private static LBstore CreateLBStoreAccount(string type)
        {
            var newAccount = new LBstore()
            {
                account = type,
                inventory = "",
                time = new DateTime(),
                list = {}
            };

            storeAccounts.Add(newAccount);
            SaveLBStoreAccounts();
            return newAccount;
        }
    }
}
