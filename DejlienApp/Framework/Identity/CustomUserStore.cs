using DejlienApp.Models;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejlienApp.Framework.Identity
{
    public class CustomUserStore : UserStore<UserAccount>
    {
        public CustomUserStore(DataContext dataContext) : base(dataContext)
        {
        }
    }
}