using System.Data.Entity;
using DejlienApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DejlienApp.Repositories
{

    public class DataContext : IdentityDbContext<UserAccount, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasOptional(u => u.Profile).WithRequired(p => p.UserAccount);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<DejlienApp.Controllers.PostIndexViewModel> PostIndexViewModels { get; set; }
    }
}
