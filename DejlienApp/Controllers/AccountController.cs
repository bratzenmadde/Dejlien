using System.Web.Mvc;
using DejlienApp.Models;
using DataLogicLayer.Models;
using DataLogicLayer.Repositories;
using System.Threading.Tasks;
using DataLogicLayer.Models.Identity;
using Microsoft.AspNet.Identity;
using System;
using DataLogicLayer.Framework.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.IO;
using System.Web;
using System.Data.Entity;

namespace DejlienApp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager applicationSignInManager;
        private AccountUserManager accountUserManager;
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(ApplicationSignInManager applicationSignInManager, AccountUserManager accountUserManager,
            IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
            this.accountUserManager = accountUserManager;
            this.applicationSignInManager = applicationSignInManager;
        }
        public AccountUserManager UserManager
        {
            get
            {
                return accountUserManager ?? Request.GetOwinContext().GetUserManager<AccountUserManager>();
            }
            private set
            {
                accountUserManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return applicationSignInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                applicationSignInManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAccount { UserName = model.Email, Email = model.Email };
                var result = await accountUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await applicationSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.SingleOrDefault());
                }

            }

            return View(model);
        }

        [Authorize]
        public ActionResult ModifyProfile()
        {
            var userId = User.Identity.GetUserId();
            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                var userProfile = currentUser.Profile;

                return View(userProfile);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ModifyProfile([Bind(Exclude = "UserPhoto")]Profile profile)
        {

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                using (var db = new DataContext())
                {
                    var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                    var userProfile = currentUser.Profile;
                    
                    byte[] imageData = null;
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase poImgFile = Request.Files["UserPhoto"];

                        using (var binary = new BinaryReader(poImgFile.InputStream))
                        {
                            imageData = binary.ReadBytes(poImgFile.ContentLength);
                        }
                    }
                    
                    profile.UserPhoto = imageData;

                    if (userProfile != null)
                    {
                        userProfile.Name = profile.Name;
                        userProfile.Age = profile.Age;
                        userProfile.Location = profile.Location;
                        userProfile.SearchingFor = profile.SearchingFor;
                        userProfile.Gender = profile.Gender;
                        userProfile.UserPhoto = profile.UserPhoto;
                        userProfile.Description = profile.Description;
                        userProfile.Visible = profile.Visible;

                        db.Entry(userProfile).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    {
                        var user = db.Users.First(c => c.Id.ToString() == userId);
                        user.Profile = profile;
                        db.SaveChanges();
                    }
                }

                ModelState.Clear();
            }

            return RedirectToAction("PersonalUserSite", "Account");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var result = await applicationSignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
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

        //View the current user profile
        public ActionResult PersonalUserSite()
        {
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Include(p => p.Profile.Posts).Single(c => c.Id.ToString() == userId);
                var profileinfo = currentUser.Profile;


                if (profileinfo != null)
                {
                    profileinfo.Posts = db.Posts.Where(p => p.Receiver.Id == profileinfo.Id).ToList();
                    var pwm = new ProfileViewModel();
                    pwm.Profile = profileinfo;
                    pwm.PostIndexViewModel = new PostIndexViewModel();
                    pwm.PostIndexViewModel.Id = currentUser.Id;

                    return View(pwm);
                }
                else
                {
                    return View("ModifyProfile");
                }
            }
        }

        //Shows profile for another user
        [Authorize]
        public ActionResult VisitProfile(int ProfileId)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                using (var db = new DataContext())
                {
                    var currentUser = db.Users.Include(p => p.Profile.Posts).Single(c => c.Id.ToString() == userId);
                    var visitUser = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();
                    visitUser.Posts = db.Posts.Where(p => p.Receiver.Id == ProfileId).ToList();
                    var con = db.Contacts.Where(c => c.User.Id == ProfileId).ToList();
                    var ct = db.Contacts.Where(x => x.User.Id == ProfileId).SingleOrDefault(q => q.Friend.Id == currentUser.Id);

                    if (currentUser.Profile != null)
                    {
                        var pwm = new ProfileViewModel();
                        pwm.Profile = visitUser;
                        pwm.PostIndexViewModel = new PostIndexViewModel();
                        pwm.PostIndexViewModel.Id = currentUser.Id;
                        pwm.ContactViewModel = new ContactViewModel();
                        pwm.ContactViewModel.Id = visitUser.Id;
                        pwm.ContactViewModel.Contacts = con;
                        pwm.ContactViewModel.Contact = ct;

                        return View("PersonalUserSite", pwm);
                    }
                    else
                    {
                        return View("ModifyProfile");
                    }

                }
            }
            catch (Exception ex)
            {
                return View("Error", "Shared", new HandleErrorInfo(ex, "Home", "Index"));
            }
        }

        public ActionResult LogOff()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //Sets userphoto for user
        [Authorize]
        public FileContentResult UserPhotos(int ProfileId)
        {
            using (var db = new DataContext())
            {
                var userProfile = db.Profiles.Where(p => p.Id == ProfileId).FirstOrDefault();

                if (userProfile.UserPhoto != null && userProfile.UserPhoto.Length > 0)
                {
                    return new FileContentResult(userProfile.UserPhoto, "image/jpeg");
                }
                else
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/pixel.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");
                }

            }

        }

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var id = User.Identity.GetUserId();
            int userId = Int32.Parse(id);
            var result = await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}