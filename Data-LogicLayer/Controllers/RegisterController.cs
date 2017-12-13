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
        // GET: Register
        public ActionResult Register()
        {
            var newUser = new User();
            return View(newUser);
        }
    }
}