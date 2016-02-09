using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWINAuth.Services
{
    public class ChallengeResult:HttpUnauthorizedResult
    {
        public ChallengeResult(string provider,string redirectUri)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
        }

        public string LoginProvider { get;  set; }
        public string RedirectUri { get;  set; }


        public override void ExecuteResult(ControllerContext context)
        {
            //Authentication.Challange --> 401 olan kodu otomatik olarak 302 redirect e çevirir-- logine yeniden yönlencez yani
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri};
            context.HttpContext.GetOwinContext().Authentication.Challenge(properties,LoginProvider);
        }

    }
}