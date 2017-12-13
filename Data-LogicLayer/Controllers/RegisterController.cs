using Data_LogicLayer.Repositories;
using DejlienApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class RegisterController : Controller
    {
        private DataContext dataContext = new DataContext();
        /*public ActionResult Register()
        {
            var newUser = new User();
            return View(newUser);
        }*/

        public ActionResult Create(User user)
        {
            dataContext.Users.Add(user);
            return View(); //Vyn för att fylla profiluppgifter.

        }
    }
}