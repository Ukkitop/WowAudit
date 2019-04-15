using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wowAudit.Models;
using wowAudit.ApiMethods;

namespace wowAudit.AuditMethods
{
    public class guildComposition
    {
        public static List<playerProfile> GetGuildComposition(string composition, string realm, string region)
        {
            AccessToken token = authMethods.GetAccessToken();
            List<playerProfile> membersList = new List<playerProfile>();
            List<string> membersToAudit = composition.Split(',').ToList<string>();
            foreach(string member in membersToAudit)
            {
                membersList.Add(Task<playerProfile>.Factory.StartNew(() => ApiMethods.getPlayerInfo.getPlayerinfo(token, member, realm, region)).Result);
            }
            //Task<playerProfile>.Factory.StartNew(() => getPlayerinfo(token, name, realm, region))

            return membersList;
        }
    }
}
