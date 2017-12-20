using System.Data.Entity;
using DejlienApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DejlienApp.Repositories
{

    public class DataContext : IdentityDbContext<UserAccount>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasOptional(u => u.Profile).WithRequired(p => p.UserAccount);

            base.OnModelCreating(modelBuilder);
        }
        //public DbSet<UserAccount> UserAccounts { get; set; }
        // You don't need to add serAccount and Role 
        // since automatically added by inheriting form IdentityDbContext<UserAccount>
        // Kommentaren från guiden på stack overflow och föreläsning 3

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
