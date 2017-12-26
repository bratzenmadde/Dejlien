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
    public class ApplicationSignInManager : SignInManager<UserAccount, int>
    {
        public ApplicationSignInManager(AccountUserManager userAccountManager, IAuthenticationManager authenticationManager)
            : base(userAccountManager, authenticationManager)
        {
        }

        
    }
}