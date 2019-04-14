using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wowAudit.Models;
using wowAudit.ApiMethods;

namespace wowAudit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult GuildSearchForm(GuildModel GuildRealmResponce)
        {
            AccessToken token = ApiMethods.authMethods.GetAccessToken();
            ViewBag.MembersDictionary = ApiMethods.GetGuildMembers.getMembersDictionary(token, GuildRealmResponce.guildName, GuildRealmResponce.guildRealm, GuildRealmResponce.guildRegion);
            return View();
        }

        public IActionResult PlayerDetails(GuildModel GuildRealmResponce)
        {
            AccessToken token = ApiMethods.authMethods.GetAccessToken();
            ViewBag.PlayerInformation = ApiMethods.getPlayerInfo.getPlayerinfo(token, GuildRealmResponce.guildName, GuildRealmResponce.guildRealm, GuildRealmResponce.guildRegion);
            return View();
        }
        public IActionResult PlayerDetailsGuildList(string name, string realm, string region)
        {
            AccessToken token = ApiMethods.authMethods.GetAccessToken();
            
            Task<playerProfile> playerInfoTask = Task<playerProfile>.Factory.StartNew(() => ApiMethods.getPlayerInfo.getPlayerinfo(token, name, realm, region));
            Task.WaitAll(playerInfoTask);
            ViewBag.PlayerInformation = playerInfoTask.Result;
            return View("PlayerDetails");
        }
    }
}
