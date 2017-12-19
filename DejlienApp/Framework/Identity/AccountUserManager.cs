using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejlienApp.Models.Identity
{
    public class UserAccountManager:UserManager<UserAccount>
    {
        public UserAccountManager(IUserStore<UserAccount> store)
        : base(store)
    {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static UserAccountManager Create(
            IdentityFactoryOptions<UserAccountManager> options, IOwinContext context)
        {
            var manager = new UserAccountManager(
                new UserStore<UserAccount>(context.Get<DataContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}