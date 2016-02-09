using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OWINAuth.Models;
using OWINAuth.Models.Model;
using OWINAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OWINAuth.Controllers
{
    public class AccountController : BaseController
    {
        
        public ActionResult SignOut()
        {
            _userManager.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie,DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToAction("login");
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult FacebookLogin()
        {
            return new ChallengeResult("Facebook", Url.Action("ExternalLoginCallBack", "Account"));
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback()
        {
            var authenticationResult = await _userManager.Authentication.AuthenticateAsync(DefaultAuthenticationTypes.ExternalCookie);

            if (authenticationResult!=null)
            {
                //eğer accessToken'a ihtiyaç  varsa çağırılabilir.
                //aşağıda facebook client sdk sı ile access_token üzerinden facebook tan data çekebiliriz. Şuan ihtiyaç yok
                //var accessToken = authenticationResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "access_token:facebook").Value;

                //var fb = new FacebookClient(accessToken);

                //var userInfo = fb.Get("me");

                return RedirectToAction("index", "home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var _user = _userManager.LoginUser(model);

                if (_user != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Email, model.Email));
                    claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, _user.Role));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                    _userManager.Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);

                    return RedirectToAction("index", "home");

                }

            }

            return View();
        }
    }
}