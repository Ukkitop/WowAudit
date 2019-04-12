using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class AzeritePower
    {
        public int id { get; set; }
        public int tier { get; set; }
        public int spellId { get; set; }
        public int bonusListId { get; set; }
    }
}
