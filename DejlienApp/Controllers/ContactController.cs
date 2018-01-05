using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using DejlienApp.Models;
using System.Data.Entity;

namespace DejlienApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact for current user
        public ActionResult ListOfContacts()
        {
            var currentUserId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                // gets the current user profile
                var currentUser = db.Users.Include(p => p.Profile).Include(c => c.Profile.Contacts).Single(c => c.Id.ToString() == currentUserId);
                var contacts = currentUser.Profile.Contacts;
                    //.Where(c => currentUser.Profile.Id.ToString() == currentUserId);

                return View(contacts);
            }
        }

        [Authorize]
        public ActionResult FriendRequest(int ProfileId, string requestButton)
        {
            // Current user
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                var userProfile = currentUser.Profile;

                var visitUserProfile = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();

                Contact contact = new Contact();
                //contact.User = userProfile;
                contact.Request = true;
                contact.Accept = false;
                contact.Friend = visitUserProfile;

                db.Contacts.Add(contact);
                db.SaveChanges();

                //var pwm = new ProfileViewModel();
                //pwm.Profile = visitedProfile;
                //pwm.PostIndexViewModel = new PostIndexViewModel();
                //pwm.PostIndexViewModel.Id = currentUserProfile.Id;
                //pwm.PostIndexViewModel.Posts = visitedUserProfiel.Posts;

                //return View("PersonalUserSite", pwm);
                return RedirectToAction("VisitProfile", "Account");
            }   
        }
    }
}