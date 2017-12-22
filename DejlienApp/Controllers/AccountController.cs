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
using System.Net;
using System.IO;
using System.Web;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAccount { UserName = model.Email, Email = model.Email };
                var result = await accountUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await applicationSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Account");
                }
              
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
        public ActionResult ModifyProfile([Bind(Exclude = "UserPhoto")]Profile profile)
        {

            if (ModelState.IsValid)
            {
                // To convert the user uploaded Photo as Byte Array before save to DB
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                //Here we pass the byte array to user context to store in db 
                profile.UserPhoto = imageData;

                // logged in user id
                var userId = User.Identity.GetUserId();
                using (var db = new DataContext())
                {
                    // get user from context
                    var user = db.Users.First(c => c.Id == userId);
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
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            var result = await applicationSignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Account");
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
        public ActionResult PersonalUserSite()
        {
            var userId = User.Identity.GetUserId();
            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id == userId);
                var profileinfo = currentUser.Profile;
                return View(profileinfo);
            }
        }

        public ActionResult VisitProfile(Profile profile)
        {
           // using (var db = new DataContext())
            {
                //var showProfile = db.Profiles.Where(p => p.Id == id);
                return View("PersonalUserSite", profile);
            }    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //public FileContentResult UserPhotos()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.Identity.GetUserId();

        //        if (userId == null)
        //        {
        //            string fileName = HttpContext.Server.MapPath(@"~/Images/pixel.png");

        //            byte[] imageData = null;
        //            FileInfo fileInfo = new FileInfo(fileName);
        //            long imageFileLength = fileInfo.Length;
        //            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fs);
        //            imageData = br.ReadBytes((int)imageFileLength);

        //            return File(imageData, "image/png");

        //        }
        //        // to get the user details to load user Image 
        //        // var bdProfiles = HttpContext.GetOwinContext().Get<DataContext>();
        //        using (var db = new DataContext())
        //        {
        //            var userImage = db.Profiles.Where(x => x.UserAccount.ToString() == userId).FirstOrDefault();
        //            return new FileContentResult(userImage.UserPhoto, "image/jpeg");
        //        }

        //    }
        //    else
        //    {
        //        string fileName = HttpContext.Server.MapPath(@"~/Images/pixel.png");

        //        byte[] imageData = null;
        //        FileInfo fileInfo = new FileInfo(fileName);
        //        long imageFileLength = fileInfo.Length;
        //        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //        BinaryReader br = new BinaryReader(fs);
        //        imageData = br.ReadBytes((int)imageFileLength);
        //        return File(imageData, "image/png");

        //    }
        //}
    }
}