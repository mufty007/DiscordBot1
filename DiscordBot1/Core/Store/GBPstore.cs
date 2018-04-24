using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot1.Core.Store
{
    public class GBPstore
    {
        public string account { get; set; }

        public DateTime time { get; set; }

        public string inventory { get; set; }

        public int[] list { get; set; }
    }
}
