using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejlienApp.Models;
using DejlienApp.Repositories;

namespace DejlienApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (DataContext db = new DataContext())
            {
                return View(db.UserAccounts.ToList());
            }
                
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (DataContext db = new DataContext())
                {
                    db.UserAccounts.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
            }
            return RedirectToAction("ModifyProfile");
        }

        public ActionResult ModifyProfile(UserAccount account)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyProfile(Profile profile)
        {
            if(ModelState.IsValid)
            {
                using (DataContext db = new DataContext())
                {
                    db.Profiles.Add(profile);
                    db.SaveChanges();
                }
                ModelState.Clear();
            }
            return RedirectToAction("LoggedIn");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (DataContext db = new DataContext())
            {
                var usr = db.UserAccounts.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if(Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}