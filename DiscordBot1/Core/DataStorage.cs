using DiscordBot1.Core.UserAccounts;
using DiscordBot1.Core.BankAccounts;
using DiscordBot1.Core.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBot1.Core
{
    public static class DataStorage
    {
        //User Accounts
        public static void SaveUserAccounts(IEnumerable<UserAccount> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<UserAccount> LoadUserAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<UserAccount>>(json);
        }

        internal static void SaveBankAccounts(List<GBPstore> storeAccounts, string accountsFile)
        {
            throw new NotImplementedException();
        }

        //Bank Account
        public static void SaveBankAccounts(IEnumerable<BankAccount> bankAccount, string filePath)
        {
            string json = JsonConvert.SerializeObject(bankAccount, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<BankAccount> LoadBankAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<BankAccount>>(json);
        }

        //GBPstore
        public static void SaveGBPstoreAccounts(IEnumerable<GBPstore> GBPBankAccount, string filePath)
        {
            string json = JsonConvert.SerializeObject(GBPBankAccount, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<GBPstore> LoadGBPstoreAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GBPstore>>(json);
        }

        //LBstore
        public static void SaveLBstoreAccounts(IEnumerable<LBstore> LBBankAccount, string filePath)
        {
            string json = JsonConvert.SerializeObject(LBBankAccount, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<LBstore> LoadLBstoreAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<LBstore>>(json);
        }

        //UserInventory
        public static void SaveUserInventory(IEnumerable<UserInventory> userInventory, string filePath)
        {
            string json = JsonConvert.SerializeObject(userInventory, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<UserInventory> LoadUserInventory(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<UserInventory>>(json);
        }

        //Check for save
        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
