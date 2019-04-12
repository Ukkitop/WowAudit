using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wowAudit.Models;
using RestSharp;
using Newtonsoft;
using Newtonsoft.Json;

namespace wowAudit.ApiMethods
{
    public class authMethods
    {
        public static AccessToken GetAccessToken()
        {
            string ClientID = "1ccea2f16d0c430bb7cbcf547ab6e289";
            string ClientSecret = "FuZJuVXuJwpoNaGS5fWpY5Sm45MtebHZ";
            string authURL = "https://eu.battle.net/oauth/token";
            var client = new RestClient(authURL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}", ClientID, ClientSecret), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            // Console.WriteLine(response.Content);
            AccessToken deserializedToken = JsonConvert.DeserializeObject<AccessToken>(response.Content);
            // Console.WriteLine(String.Format("token:{0}, token type: {1}, expire: {2}", deserializedToken.access_token, deserializedToken.token_type, deserializedToken.expires_in));
            return deserializedToken;
        }
    }
}
