using DejlienApp.Models;
using DejlienApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new DataContext())
            {
                var users = db.Profiles.ToList();
                return View(users);
            }     
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}