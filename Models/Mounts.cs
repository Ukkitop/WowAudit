using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class Mounts
    {
        public int numCollected { get; set; }
        public int numNotCollected { get; set; }
        public IList<MountModel> collected { get; set; }
    }
}
