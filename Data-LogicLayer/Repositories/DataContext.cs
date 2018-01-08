using System.Data.Entity;
using DataLogicLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataLogicLayer.Repositories
{

    public class DataContext : IdentityDbContext<UserAccount, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasOptional(u => u.Profile).WithRequired(p => p.UserAccount);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<DataLogicLayer.Models.PostIndexViewModel> PostIndexViewModels { get; set; }
    }
}
