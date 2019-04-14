using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wowAudit.Models;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace wowAudit.ApiMethods
{
    public class getPlayerInfo
    {

        public static Dictionary<string, string> getPlayerBaseInfo(string name, string realm, string region)
        {
            Dictionary<string, string> playerBaseInfo = new Dictionary<string, string>();
            string apiPath = String.Format("https://raider.io/api/v1/characters/profile");
            var client = new RestClient(apiPath);
            var request = new RestRequest(Method.GET);
            request.AddParameter("region", region);
            request.AddParameter("realm", realm);
            request.AddParameter("name", name);            
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;            
            JObject playerInfo = JObject.Parse(response.Content);
            foreach (JProperty property in playerInfo.Properties())
            {
                playerBaseInfo.Add(property.Name, property.Value.ToString());
            }


            return playerBaseInfo;

            


        }
        

        public static float getRaiderIOScore(string name, string realm, string region)
        {
            float score = 0;

            string apiPath = String.Format("https://raider.io/api/v1/characters/profile");
            var client = new RestClient(apiPath);
            var request = new RestRequest(Method.GET);
            request.AddParameter("region", region);
            request.AddParameter("realm", realm);
            request.AddParameter("name", name);
            request.AddParameter("fields", "mythic_plus_scores_by_season:current");
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return 0;
            dynamic obj = JObject.Parse(response.Content);
            JArray scoresArray = JArray.Parse(obj.mythic_plus_scores_by_season.ToString());
            foreach (JObject jsonObject in scoresArray.Children<JObject>())
            {
                foreach (JProperty CharProperty in jsonObject.Properties())
                {
                    switch (CharProperty.Name)
                    {
                        case "scores":
                            JObject characterObject = JObject.Parse(CharProperty.Value.ToString());
                            score = float.Parse(characterObject["all"].ToString());
                            break;
                        default:
                            break;
                    }
                }
            } 
            return score;
        }

        public static List<Progress> getPlayerProgress(string name, string realm, string region)
        {
            List<Progress> progressList = new List<Progress>();

            string apiPath = String.Format("https://raider.io/api/v1/characters/profile");
            var client = new RestClient(apiPath);
            var request = new RestRequest(Method.GET);
            request.AddParameter("region", region);
            request.AddParameter("realm", realm);
            request.AddParameter("name", name);
            request.AddParameter("fields", "raid_progression");
            request.AddHeader("Accept", "application/json");

            //request.AddHeader("authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return null;
            dynamic obj = JObject.Parse(response.Content);

            JObject progressArray = JObject.Parse(obj.raid_progression.ToString());

            
                foreach (JProperty CharProperty in progressArray.Properties())
                {
                    Progress progress = new Progress();
                    progress.dungeon = CharProperty.Name;
                    string totalBosses = CharProperty.Value["total_bosses"].ToString();
                    progress.normalProgress = CharProperty.Value["normal_bosses_killed"].ToString() + "/" + totalBosses;
                    progress.heroicProgress = CharProperty.Value["heroic_bosses_killed"].ToString() + "/" + totalBosses;
                    progress.mythicProgress = CharProperty.Value["mythic_bosses_killed"].ToString() + "/" + totalBosses;
                    progressList.Add(progress);
                }
            
            return progressList;
        }

        public static List<string> getWowheadURL(Items items)
        {
            List<item> itemList = items.getItemList();
            List<string> itemURLS = new List<string>();
            System.UriBuilder uriBuilder = new UriBuilder();
            
            foreach (item item in itemList)
            {
                if (item == null) continue;
                uriBuilder.Host = "en.wowhead.com";
                uriBuilder.Scheme = "https";
                try
                {
                    uriBuilder.Query = "item=" + item.id;
                    uriBuilder.Query += "&";
                    uriBuilder.Query += "bonus=";
                    foreach (int bonus in item.bonusLists)
                    {
                        uriBuilder.Query += bonus + ":";
                    }
                    if (item.id == 158075)
                    {
                        uriBuilder.Query +="&ilvl=" + item.itemLevel;
                    }
                    if(item.tooltipParams.gem0 != 0)
                    {
                        uriBuilder.Query += "&gems=" + item.tooltipParams.gem0;
                    }

                    if (item.tooltipParams.enchant != 0)
                    {
                        uriBuilder.Query += "&ench=" + item.tooltipParams.enchant;
                    }
                }
                catch (System.NullReferenceException e)
                { }

                itemURLS.Add(uriBuilder.ToString());
                
            }
            
            return itemURLS;
        }

        public static Items getPlayerItems(AccessToken token, string name, string realm, string region)
        {
            string apiPath = String.Format("https://{0}.api.blizzard.com/wow/character/{1}/{2}", region, realm, name);
            var client = new RestClient(apiPath);
            var request = new RestRequest(Method.GET);
            request.AddParameter("fields", "items");
            request.AddHeader("Accept", "application/json");

            request.AddHeader("authorization", "Bearer " + token.access_token);
            IRestResponse response = client.Execute(request);

            Items items = JsonConvert.DeserializeObject<Items>(JObject.Parse( response.Content)["items"].ToString());

            return items;
        }
        public static playerProfile getPlayerinfo(AccessToken token, string name, string realm, string region)
        {
            playerProfile profile = new playerProfile();
            Task<Items> itemsTask = Task<Items>.Factory.StartNew(() => getPlayerItems(token, name, realm, region));
            Task<float> raiderIOTask = Task<float>.Factory.StartNew(() => getRaiderIOScore( name, realm, region));
            Task<List<Progress>> progressTask = Task<List<Progress>>.Factory.StartNew(() => getPlayerProgress(name, realm, region));
            Task<Dictionary<string, string>> baseinfoTask = Task<Dictionary<string, string>>.Factory.StartNew(() => getPlayerBaseInfo(name, realm, region));
            Task.WaitAll(itemsTask, raiderIOTask, progressTask, baseinfoTask);
            Dictionary<string, string> baseInfoDictionary = baseinfoTask.Result;            
            profile.items = itemsTask.Result;
            profile.raiderIOScore = raiderIOTask.Result;
            profile.progress = progressTask.Result;
            profile.name = baseInfoDictionary["name"];
            profile.race = baseInfoDictionary["race"];
            profile.realm = baseInfoDictionary["realm"];
            profile.gender = baseInfoDictionary["gender"];
            profile.specName = baseInfoDictionary["active_spec_name"];
            profile.thumbnail = baseInfoDictionary["thumbnail_url"];
            profile.className = baseInfoDictionary["class"];
            profile.faction = baseInfoDictionary["faction"];
            return profile;
        }
    }
}
