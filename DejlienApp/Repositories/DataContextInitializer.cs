using DejlienApp.Models;
using DejlienApp.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace DejlienApp.Repositories
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            SeedUsers(context);
            context.SaveChanges();
            base.Seed(context);
        }

        private static void SeedUsers(DataContext context)
        {
            var store = new CustomUserStore(context);
            var userManager = new AccountUserManager(store);

            var user1 = new UserAccount { UserName = "JarJarBinks@gmail.com", Email = "JarJarBinks@gmail.com" };
            var user2 = new UserAccount { UserName = "LeiaOrgana@hotmail.com", Email = "LeiaOrgana@hotmail.com" };

            var profile1 = new Profile {
                Name = "Jar Jar Binks" ,
                Age = 30, Location = "Naboo",
                SearchingFor = "Female",
                Gender = "Male",
                UserPhoto = null,
                Description = "Something about me..",
                Visible = Visible.Yes,
                UserAccount = user1
            };

            var profile2 = new Profile
            {
                Name = "Leia Organa",
                Age = 45,
                Location = "Alderaan",
                SearchingFor = "Male",
                Gender = "Female",
                UserPhoto = null,
                Description = "Something about me..",
                Visible = Visible.Yes,
                UserAccount = user2
            };

            userManager.CreateAsync(user1, "User1!").Wait();
            userManager.CreateAsync(user2, "User2!").Wait();

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
        }
    }
}