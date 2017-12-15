using DejlienApp.Repositories;
using DejlienApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class UserController : Controller
    {
        //private DataContext dataContext = new DataContext();

        //public ActionResult Create(User user)
        //{
        //    dataContext.Users.Add(user);
        //    dataContext.SaveChanges();
        //    return RedirectToAction("CreateProfile"); //Vyn för att fylla profiluppgifter.
        //}

        public ActionResult Create()
        {
            return View();
        }
    }
}