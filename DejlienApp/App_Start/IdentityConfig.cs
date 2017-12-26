using DejlienApp.Framework.Identity;
using DejlienApp.Models;
using DejlienApp.Models.Identity;
using DejlienApp.Repositories;
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

namespace DejlienApp.App_Start
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


        //app.CreatePerOwinContext(() => new DataContext());
        //app.CreatePerOwinContext<AccountUserManager>(AccountUserManager.Create);

        //////Johan har inte med detta i föreläsning 3
        ////app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
        ////    new RoleManager<Role>(
        ////        new RoleStore<Role>(context.Get<DataContext>())));

        //app.UseCookieAuthentication(new CookieAuthenticationOptions
        //{
        //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        //    LoginPath = new PathString("/Home/Login"),
        //});
    }
}