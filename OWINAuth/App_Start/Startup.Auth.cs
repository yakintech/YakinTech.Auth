using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using System.Security.Claims;

[assembly: OwinStartup(typeof(OWINAuth.App_Start.Startup))]

namespace OWINAuth.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),

            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,

            });

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = "961624033920089",
                AppSecret = "c1da6a50b6a41946ac009d3440ceccfc",
                Provider = new FacebookAuthenticationProvider
                {
                   
                    OnAuthenticated = FacebookContext =>
                    {
                        FacebookContext.Identity.AddClaim(new Claim(ClaimTypes.Name, FacebookContext.Name));
                        return Task.FromResult(true);
                    }

                    //authenticationdan sonra eğer veri alış verişi olucaksa access_token'a ihtiyaç var
                    //OnAuthenticated = FacebookContext =>
                    //{
                    //    FacebookContext.Identity.AddClaim(new Claim("access_token", FacebookContext.AccessToken));
                    //    return Task.FromResult(true);
                    //}
                }

            };

            facebookOptions.Scope.Add("user_about_me");
            app.UseFacebookAuthentication(facebookOptions);





        }
    }
}
