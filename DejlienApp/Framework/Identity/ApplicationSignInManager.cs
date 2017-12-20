using DejlienApp.Models;
using DejlienApp.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace DejlienApp.Framework.Identity
{
    public class ApplicationSignInManager : SignInManager<UserAccount, string>
    {
        public ApplicationSignInManager(AccountUserManager userAccountManager, IAuthenticationManager authenticationManager)
            : base(userAccountManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserAccount userAccount)
        {
            return UserManager.CreateIdentityAsync(userAccount, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}