using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wowAudit.ApiMethods;
using wowAudit.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wowAudit.Controllers
{
    public class mainController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            AccessToken token = authMethods.GetAccessToken();
           // ViewBag.membersDictionary = ApiMethods.GetGuildMembers.getMembersDictionary(token);
            return View();
        }
    }
}
