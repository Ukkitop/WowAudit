using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.Models
{
    public class Items
    {
        public int averageItemLevel { get; set; }
        public int averageItemLevelEquipped { get; set; }
        public item head { get; set; }
        public item neck { get; set; }
        public item shoulder { get; set; }
        public item back { get; set; }
        public item chest { get; set; }
        public item wrist { get; set; }
        public item hands { get; set; }
        public item waist { get; set; }
        public item legs { get; set; }
        public item feet { get; set; }
        public item finger1 { get; set; }
        public item finger2 { get; set; }
        public item trinket1 { get; set; }
        public item trinket2 { get; set; }
        public item mainHand { get; set; }
        public item offHand { get; set; }

        public List<item> itemList = new List<item>();

        public List<item> getItemList()
        {
            itemList.Add(head);
            itemList.Add(neck);
            itemList.Add(shoulder);
            itemList.Add(back);
            itemList.Add(chest);
            itemList.Add(wrist);
            itemList.Add(hands);
            itemList.Add(waist);
            itemList.Add(legs);
            itemList.Add(feet);
            itemList.Add(finger1);
            itemList.Add(finger2);
            itemList.Add(trinket1);
            itemList.Add(trinket2);
            itemList.Add(mainHand);
            itemList.Add(offHand);
            return itemList;
        }
    }
}
