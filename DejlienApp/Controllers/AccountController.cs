﻿using System.Web.Mvc;
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
using System.Data.Entity;

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

                    return RedirectToAction("Index", "Home");
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
                // logged in user id
                var userId = User.Identity.GetUserId();
                using (var db = new DataContext())
                {
                    var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                    var userProfile = currentUser.Profile;

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

                    if (userProfile != null)
                    {
                        userProfile.Name = profile.Name;
                        userProfile.Age = profile.Age;
                        userProfile.Location = profile.Location;
                        userProfile.SearchingFor = profile.SearchingFor;
                        userProfile.Gender = profile.Gender;
                        userProfile.UserPhoto = profile.UserPhoto;
                        userProfile.Description = profile.Description;

                        db.Entry(userProfile).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else
                    {
                        // get user from context
                        var user = db.Users.First(c => c.Id.ToString() == userId);
                        // assign UserProfileInfo to user
                        user.Profile = profile;
                        // save changes
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
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
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


        public ActionResult PersonalUserSite()
        {
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                var profileinfo = currentUser.Profile;

                if (profileinfo != null)
                {
                    return View(profileinfo);
                }
                else
                {
                    return View("ModifyProfile");
                }
            }
        }

        public ActionResult VisitProfile(int ProfileId)
        {
            using (var db = new DataContext())
            {
                var visitUser = db.Profiles.Where(p => p.Id == ProfileId).SingleOrDefault();

                return View("PersonalUserSite", visitUser);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public FileContentResult UserPhotos(int ProfileId)
        {

            using (var db = new DataContext())
            {
                var userProfile = db.Profiles.Where(p => p.Id == ProfileId).FirstOrDefault();

                if (userProfile.UserPhoto != null)
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
    }
}