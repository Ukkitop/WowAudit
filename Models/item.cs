using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int quality { get; set; }
        public int itemLevel { get; set; }
        public TooltipParams tooltipParams { get; set; }
        public IList<Stat> stats { get; set; }
        public int armor { get; set; }
        public string context { get; set; }
        public IList<int> bonusLists { get; set; }
        public int displayInfoId { get; set; }
        public int artifactId { get; set; }
        public int artifactAppearanceId { get; set; }
        public IList<object> artifactTraits { get; set; }
        public IList<object> relics { get; set; }
        public Appearance appearance { get; set; }
        public AzeriteItem azeriteItem { get; set; }
        public AzeriteEmpoweredItem azeriteEmpoweredItem { get; set; }
    }
}
