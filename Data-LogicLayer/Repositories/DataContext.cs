using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DejlienApp.Framework.Logging;
using DejlienApp.Models;
using System.Data.Entity;
//using Microsoft.Aspnet.Identity.EntityFramework;

namespace Data_LogicLayer.Repositories
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
