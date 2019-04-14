using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wowAudit.ConstantEnums
{
    public class ClassConsts
    {
        public static string getClassString(int classNumber)
        {
            switch (classNumber) {                
                case 1: {
                        return "Warrior";                       
                    };
                case 2: {
                        return "paladin";                        
                    };
                case 3: {
                        return "hunter";
                    };
                case 4: {
                        return "rogue";
                    };
                case 5: {
                        return "priest";
                    };
                case 6: {
                        return "Death Knight";
                    };
                case 7: {
                        return "shaman";
                    };
                case 8: {
                        return "mage";
                    };
                case 9: {
                        return "Warlock";
                    };
                case 10: {
                        return "monk";
                    };
                case 11:
                    {
                        return "druid";
                    };

                default:
                    {
                        return "Demon Hunter";
                    }; }
             
        }
    }
    
}
