using Data_LogicLayer.Repositories;
using Data_LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Data_LogicLayer.Controllers
{
    public class RegisterController : Controller
    {
        private DataContext dataContext = new DataContext();

        [HttpPost]
        public ActionResult Create(User user)
        {
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
            return View(); //Vyn för att fylla profiluppgifter.

        }
    }
}