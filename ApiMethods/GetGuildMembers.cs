using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using RestSharp;
using wowAudit.Models;

namespace wowAudit.ApiMethods
{
    public class GetGuildMembers
    {
        public static List<guildMember> getMembersDictionary(AccessToken token, string guildName, string guildrealm, string guildRegion)
            {
            List<guildMember> membersDictionary = new List<guildMember>();
            
            string apiPath = String.Format("https://{0}.api.blizzard.com/wow/guild/{1}/{2}", guildRegion, guildrealm, guildName);
            var client = new RestClient(apiPath);
            var request = new RestRequest(Method.GET);
            request.AddParameter("fields", "members");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);

            //Console.WriteLine(response.Content + "\n\n\n\n\n");
            dynamic obj = JObject.Parse(response.Content);

            JArray membersArray = JArray.Parse(obj.members.ToString());
            foreach (JObject member in membersArray.Children<JObject>())
            {
                foreach (JProperty CharProperty in member.Properties())
                {
                    //Console.WriteLine(CharProperty.Name);
                    switch (CharProperty.Name)
                    {
                        case "character":
                            JObject characterObject = JObject.Parse(CharProperty.Value.ToString());
                            Console.WriteLine(characterObject["name"] + ": " + characterObject["realm"]);
                            guildMember guildmember = new guildMember();
                            guildmember.name = characterObject["name"].ToString();
                            guildmember.realm = characterObject["realm"].ToString();
                            guildmember.region = guildRegion;
                            guildmember.classRole = (int)characterObject["class"];
                            membersDictionary.Add(guildmember);
                            break;
                        default:
                            break;
                    }
                    
                    // Console.WriteLine(property.Name + ":" + property.Value);
                }

            }
            return membersDictionary;
            }
    }
}
