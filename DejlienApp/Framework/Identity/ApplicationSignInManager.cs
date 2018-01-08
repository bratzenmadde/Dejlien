using DejlienApp.Models;
using DejlienApp.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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