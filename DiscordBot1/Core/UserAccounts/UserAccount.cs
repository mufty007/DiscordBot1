using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot1.Core.UserAccounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }
        
        public double GBP { get; set; }

        public double Money { get; set; }

        public string mPaid { get; set; }

        public DateTime mdtPaid { get; set; }

        public string gbpPaid { get; set; }

        public DateTime gbpdtPaid { get; set; }

        public double length { get; set; }

        public string tttA { get; set; }

        public string tttB { get; set; }

        public string tttC { get; set; }

        public string tttD { get; set; }

        public string tttE { get; set; }

        public string tttF { get; set; }

        public string tttG { get; set; }

        public string tttH { get; set; }

        public string tttI { get; set; }

        public List<string> lbitems { get; set; }

        public List<int> lbamount { get; set; }

        public List<string> gbpitems { get; set; }

        public List<int> gbpamount { get; set; }

    }
}
