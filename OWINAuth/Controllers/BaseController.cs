using OWINAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWINAuth.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationUserManager _userManager;
        public BaseController()
        {
            _userManager = new ApplicationUserManager();
        }
    }
}