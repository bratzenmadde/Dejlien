using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DejlienApp.Models;
using DejlienApp.Framework;

namespace DejlienApp.Repositories
{
    public class UserRepository : Repository<UserAccount, int>
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        //internal UserAccount GetByName(string username)
        //{
        //    var user = Items.SingleOrDefault(x => x.Name.ToLower() == username.ToLower());
        //    return user;
        //}
    }
}