using DejlienApp.Models;
using DejlienApp.Models.Identity;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace DejlienApp.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DataContext());
            app.CreatePerOwinContext<UserAccountManager>(UserAccountManager.Create);

            //Johan har inte med detta i föreläsning 3
            //app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
            //    new RoleManager<Role>(
            //        new RoleStore<Role>(context.Get<DataContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}