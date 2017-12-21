using System.Web.Mvc;
using DejlienApp.Models;
using DejlienApp.Repositories;
using System.Threading.Tasks;
using DejlienApp.Models.Identity;
using Microsoft.AspNet.Identity;
using System;
using DejlienApp.Framework.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace DejlienApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationSignInManager applicationSignInManager;
        private readonly AccountUserManager accountUserManager;
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(ApplicationSignInManager applicationSignInManager, AccountUserManager accountUserManager,
            IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
            this.accountUserManager = accountUserManager;
            this.applicationSignInManager = applicationSignInManager;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAccount { UserName = model.Email, Email = model.Email };
                var result = await accountUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await applicationSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Login", "Account");
                }
                //AddErrors(result);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ModifyProfile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ModifyProfile(Profile profile)
        {
            if (ModelState.IsValid)
            {

                // logged in user id
                var userId = User.Identity.GetUserId();
                using (var db = new DataContext())
                {
                    // get user from context
                    var user = db.Users.First(c => c.Id == userId);
                    // new UserProfileInfo
                    // assign UserProfileInfo to user
                    user.Profile = profile;
                    // save changes
                    db.SaveChanges();
                }

                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl, Profile profile)
        {
            if (!ModelState.IsValid)
            {
                /*var userId = User.Identity.GetUserId();*/
                
                return RedirectToAction("CheckIfProfileExists");
            }


            var result = await applicationSignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                       // if (profile.UserAccount == null)
                            return RedirectToAction("ModifyProfile");
                        //else
                        //    return RedirectToAction("Index");
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //public ActionResult CheckIfProfileExists()
        //{
        //    return RedirectToAction("CheckIfProfileExists");
        //}
        //[Authorize]
        //[HttpPost, ValidateAntiForgeryToken]
        //public async Task<ActionResult> CheckIfProfileExists(LoginModel model, /*string returnUrl,*/ Profile profile)
        //{
        //    var current = await accountUserManager.FindByIdAsync(User.Identity.GetUserId());
        //    using (var db = new DataContext())
        //    {
        //        if (db.Profiles.Any(x => x.UserAccount == current))
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return RedirectToAction("ModifyProfile");
        //        }
        //    }
        //}

        [Authorize]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }

}