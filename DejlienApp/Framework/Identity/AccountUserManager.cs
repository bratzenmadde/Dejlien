using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DejlienApp.Models.Identity
{
    public class AccountUserManager : UserManager<UserAccount, int>
    {
        public AccountUserManager(IUserStore<UserAccount, int> store)
        : base(store)
        {
        }
        public static AccountUserManager Create(
        IdentityFactoryOptions<AccountUserManager> options, IOwinContext context)
        {
            var manager = new AccountUserManager(
                new CustomUserStore(context.Get<DataContext>()));
            manager.UserValidator = new UserValidator<UserAccount, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }

    }
}