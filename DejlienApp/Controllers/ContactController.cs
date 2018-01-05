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
                var contacts = db.Contacts.Include(f => f.Friend).Where(c => c.User.Id.ToString() == currentUserId);
                var con = contacts.ToList();

                return View(con);
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
                var visitUserProfile = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();


                Profile profile = new Profile
                {
                    Id = visitUserProfile.Id,
                    Name = visitUserProfile.Name,
                    Age = visitUserProfile.Age,
                    Location = visitUserProfile.Location,
                    Gender = visitUserProfile.Gender,
                    SearchingFor = visitUserProfile.SearchingFor,
                    UserPhoto = visitUserProfile.UserPhoto,
                    Description = visitUserProfile.Description,
                    Visible = visitUserProfile.Visible,
                    UserAccount = visitUserProfile.UserAccount
                };

                Contact contact = new Contact
                {
                    User = currentUser.Profile,
                    Request = true,
                    Accept = false,
                    Friend = profile
                };

                //currentUser.Profile.Contacts.Add(contact);

                db.Contacts.Add(contact);
                db.SaveChanges();



                //var pwm = new ProfileViewModel();
                //pwm.Profile = visitedProfile;
                //pwm.PostIndexViewModel = new PostIndexViewModel();
                //pwm.PostIndexViewModel.Id = currentUserProfile.Id;
                //pwm.PostIndexViewModel.Posts = visitedUserProfiel.Posts;

                //return View("PersonalUserSite", pwm);
                return RedirectToAction("VisitProfile");
            }   
        }
    }
}