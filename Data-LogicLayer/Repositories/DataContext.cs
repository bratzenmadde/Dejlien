using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DejlienApp.Framework.Logging;
using System.Data.Entity;
using Data_LogicLayer.Models;
//using Microsoft.Aspnet.Identity.EntityFramework;

namespace Data_LogicLayer.Repositories
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
