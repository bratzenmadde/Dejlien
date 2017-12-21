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
                using (DataContext db = new DataContext())
                {
                    var currentUserId = User.Identity.GetUserId(); //var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId);
                    db.Profiles.Add(profile); 
                    db.SaveChangesAsync();

                }
                ModelState.Clear();
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/")
        {
            var externalLogins = authenticationManager.GetExternalAuthenticationTypes();

            return View(new LoginModel { /*ExternalLogins = externalLogins, ReturnUrl = returnUrl*/ });

        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                //model.ExternalLogins = authenticationManager.GetExternalAuthenticationTypes();

                return View(model);
            }


            var result = await applicationSignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        return RedirectToAction("ModifyProfile");   
                    }
                    
                //return RedirectToLocal(returnUrl);
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

        [Authorize]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }

}