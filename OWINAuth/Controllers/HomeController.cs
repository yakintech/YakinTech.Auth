using Microsoft.AspNet.Identity;
using OWINAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OWINAuth.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.CurentUser =_userManager.CurrentUser;

            return View();
        }
    }
}