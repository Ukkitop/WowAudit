using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class MountModel
    {
        public string name { get; set; }
        public int spellId { get; set; }
        public int creatureId { get; set; }
        public int itemId { get; set; }
        public int qualityId { get; set; }
        public string icon { get; set; }
        public bool isGround { get; set; }
        public bool isFlying { get; set; }
        public bool isAquatic { get; set; }
        public bool isJumping { get; set; }
    }
}
