using DejlienApp.Framework;
using Microsoft.AspNet.Identity.EntityFramework;


namespace DejlienApp.Models
{
    public class UserAccount:IdentityUser, IEntity<string>
    {
        public Profile Profile { get; set; }
    }
}