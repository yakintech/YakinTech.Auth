using Microsoft.Owin.Security;
using OWINAuth.Models.Context;
using OWINAuth.Models.Entities;
using OWINAuth.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWINAuth.Services
{
    public class ApplicationUserManager
    {
        public  IAuthenticationManager Authentication
        {
            get { return HttpContext.Current.GetOwinContext().Authentication; }
        }

        public  string CurrentUser
        {
            get { return HttpContext.Current.GetOwinContext().Authentication.User.Identity.Name; }
        }

        public  ApplicationUser LoginUser(LoginModel model)
        {
            using (ProjectContext _context = new ProjectContext())
            {
                return _context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            }
        }
    }
}