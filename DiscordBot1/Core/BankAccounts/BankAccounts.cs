using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot1.Core.BankAccounts
{
    public static class BankAccounts
    {
        private static List<BankAccount> bankAccounts;

        private static string accountsFile = "Resources/bankAccounts.json";

        static BankAccounts()
        {
            if (DataStorage.SaveExists(accountsFile))
            {
                bankAccounts = DataStorage.LoadBankAccounts(accountsFile).ToList();
            }
            else
            {
                bankAccounts = new List<BankAccount>();
                SaveBankAccounts();
            }
        }

        public static void SaveBankAccounts()
        {
            DataStorage.SaveBankAccounts(bankAccounts, accountsFile);
        }

        public static BankAccount GetAccount(string type)
        {
            return GetOrCreateBankAccount(type);
        }

        private static BankAccount GetOrCreateBankAccount(string type)
        {
            var result = from a in bankAccounts
                         where a.accountType == type
                         select a;

            var account = result.FirstOrDefault();
            if (account == null) account = CreateBankAccount(type);
            return account;
        }

        private static BankAccount CreateBankAccount(string type)
        {
            var newAccount = new BankAccount()
            {
                accountType = type,
                totalFunds = 0
            };

            bankAccounts.Add(newAccount);
            SaveBankAccounts();
            return newAccount;
        }
    }
}
