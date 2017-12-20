using System.Web.Mvc;
using DejlienApp.Models;
using DejlienApp.Repositories;
using System.Threading.Tasks;
using DejlienApp.Models.Identity;
using Microsoft.AspNet.Identity;
using System;
using DejlienApp.Framework.Identity;

namespace DejlienApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationSignInManager applicationSignInManager;
        private readonly AccountUserManager userAccountManager;

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAccount { UserName = model.Email, Email = model.Email };
                var result = await userAccountManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await applicationSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Book");
                }
                //AddErrors(result);
            }

            return View(model);
        }


        // [HttpPost]
        // public ActionResult Register(UserAccount account)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         using (DataContext db = new DataContext())
        //         {
        //             db.Users.Add(account);
        //             db.SaveChanges();
        //         }
        //         ModelState.Clear();
        //     }
        //     return View();
        ///*     return RedirectToAction("ModifyProfile", new { id = account.UserId })*/;
        // }

        public ActionResult ModifyProfile(int id)
        {
            return View();
        }
        [HttpPost]
        //public ActionResult ModifyProfile(Profile profile, UserAccount Account, int id)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        using (DataContext db = new DataContext())
        //        {
        //            db.UserAccounts.Single(m=> m.UserId == id).Profile = profile;
        //            db.SaveChanges();
                    
        //        }
        //        ModelState.Clear();
        //    }

        //    return RedirectToAction("LoggedIn");
        //}

        //Login
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(UserAccount user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        //        var authManager = HttpContext.GetOwinContext().Authentication;

        //        AppUser usr = userManager.Find(user.Username, user.Password);
        //        if (usr != null)
        //        {
        //            var ident = userManager.CreateIdentity(usr,
        //                DefaultAuthenticationTypes.ApplicationCookie);
        //            authManager.SignIn(
        //                new AuthenticationProperties { IsPersistent = false }, ident);
        //           // return Redirect(user.ReturnUrl ?? Url.Action("Index", "Home"));
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid username or password");
        //    return View(user);


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
        //}

        public ActionResult LoggedIn()
        {
                    return View();
        }
    }
}