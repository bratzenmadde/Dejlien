using System.Data.Entity;
using DejlienApp.Models;

namespace DejlienApp.Repositories
{

    public class DataContext : DbContext
    {
        public DataContext() : base("DataContext")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
