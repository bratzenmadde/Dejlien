using DejlienApp.Framework;
using DejlienApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejlienApp.Repositories
{
    public class ProfileRepository : Repository<Profile, int>
    {
        public ProfileRepository(DataContext context) : base(context)
        {

        }

        //public List<Profile> GetAllIncludeGenre()
        //{
        //    return Items.Include(x => x.Genre).ToList();
        //}
    }
}