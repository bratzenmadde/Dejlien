using DataLogicLayer.Framework.Identity;
using DataLogicLayer.Models;
using DataLogicLayer.Models.Identity;
using DataLogicLayer.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Unity;
using Owin;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

namespace DataLogicLayer.App_Start
{
    public class IdentityConfig
    {

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => UnityConfig.Container.Resolve<AccountUserManager>());
            app.CreatePerOwinContext(() => UnityConfig.Container.Resolve<ApplicationSignInManager>());
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<AccountUserManager, UserAccount, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager), getUserIdCallback: (id) =>(id.GetUserId<int>()))
                }
            });
        }
    }
}