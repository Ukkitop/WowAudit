using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class Reputation
    {
        public int id { get; set; }
        public string name { get; set; }
        public int standing { get; set; }
        public int value { get; set; }
        public int max { get; set; }
    }
}
