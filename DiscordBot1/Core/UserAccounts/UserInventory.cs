using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot1.Core.UserAccounts
{
    public class UserInventory
    {
        public ulong ID { get; set; }

        public string CurrencyType { get; set; }

        public string[] items { get; set; }

        public int[] amount { get; set; }
    }
}
