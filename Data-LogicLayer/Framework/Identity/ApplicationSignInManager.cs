using DataLogicLayer.Models;
using DataLogicLayer.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DataLogicLayer.Framework.Identity
{
    public class ApplicationSignInManager : SignInManager<UserAccount, int>
    {
        public ApplicationSignInManager(AccountUserManager userAccountManager, IAuthenticationManager authenticationManager)
            : base(userAccountManager, authenticationManager)
        {
        }
    }
}