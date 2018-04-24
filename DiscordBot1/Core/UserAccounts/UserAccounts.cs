using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace DiscordBot1.Core.UserAccounts
{
    public static class UserAccounts
    {
        private static List<UserAccount> accounts;

        private static string accountsFile = "Resources/accounts.json";

        static UserAccounts()
        {
            if(DataStorage.SaveExists(accountsFile))
            {
                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }

        public static void SaveAccounts()
        {
            DataStorage.SaveUserAccounts(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }
        public static UserAccount GetAccount(ulong user)
        {
            return GetOrCreateAccount(user);
        }

        private static UserAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;

            var account = result.FirstOrDefault();
            if(account == null) account = CreateUserAccount(id);
            return account;
        }

        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                GBP = 0,
                Money = 0,
                mPaid = "",
                mdtPaid = new DateTime(),
                gbpPaid = "",
                gbpdtPaid = new DateTime(),
                length = 0,
                tttA = "_",
                tttB = "_",
                tttC = "_",
                tttD = "_",
                tttE = "_",
                tttF = "_",
                tttG = "_",
                tttH = "_",
                tttI = "_",
                lbitems = new List<string>(),
                lbamount = new List<int>(),
                gbpitems = new List<string>(),
                gbpamount = new List<int>()

            };

            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}
