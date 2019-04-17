using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    
        public class playerProfile
        {
            public long lastModified { get; set; }
            public string name { get; set; }
            public string realm { get; set; }
            public string battlegroup { get; set; }
            public string className { get; set; }
            public string race { get; set; }
            public string gender { get; set; }
            public int level { get; set; }
            public int achievementPoints { get; set; }
            public string thumbnail { get; set; }
            public string specName { get; set; }
            public string faction { get; set; }
            public Items items { get; set; }
            public int totalHonorableKills { get; set; }
            public float raiderIOScore { get; set; }
            public List<Progress> progress { get; set; } 
            public Mounts mounts { get; set; }
        }
    }
