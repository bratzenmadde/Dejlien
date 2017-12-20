using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejlienApp.Models;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity.Owin;
using DejlienApp.Framework.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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
            return RedirectToAction("ModifyProfile", new { id=account.UserId});
        }

        public ActionResult ModifyProfile(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyProfile(Profile profile, UserAccount Account, int id)
        {
            if(ModelState.IsValid)
            {
                using (DataContext db = new DataContext())
                {
                    db.UserAccounts.Single(m=> m.UserId == id).Profile = profile;
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
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AccountUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser usr = userManager.Find(user.Username, user.Password);
                if (usr != null)
                {
                    var ident = userManager.CreateIdentity(usr,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                   // return Redirect(user.ReturnUrl ?? Url.Action("Index", "Home"));
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(user);


            /*using (DataContext db = new DataContext())
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
            return View();*/
        }

        public ActionResult LoggedIn()
        {
                    return View();
        }
    }
}