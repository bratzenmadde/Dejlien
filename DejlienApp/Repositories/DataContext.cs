using System.Data.Entity;
using DejlienApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using DejlienApp.Framework.Identity;

namespace DejlienApp.Repositories
{

    public class DataContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasOptional(u => u.Profile).WithRequired(p => p.UserAccount);
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
