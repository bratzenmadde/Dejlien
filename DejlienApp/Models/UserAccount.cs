using DejlienApp.Framework;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    public class UserAccount:IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IEntity<int>
    {
        public virtual Profile Profile { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserAccount, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
            //return UserManager.CreateIdentityAsync(userAccount, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
    public class CustomUserLogin : IdentityUserLogin<int> { }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim <int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole (string name) { Name = name; }

    }

    public class CustomUserStore : UserStore<UserAccount, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(DataContext context) : base(context)
        {
        }
    }
    public class CustomRoleStore: RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(DataContext context) : base(context)
        {
        }
    }
}