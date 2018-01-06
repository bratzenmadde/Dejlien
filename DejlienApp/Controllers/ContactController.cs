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
        public ActionResult ListOfContacts()
        {
            var currentUserId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Include(p => p.Profile).Include(c => c.Profile.Contacts).Single(c => c.Id.ToString() == currentUserId);
                var contacts = db.Contacts.Include(f => f.Friend).Where(c => c.User.Id.ToString() == currentUserId);
                var con = contacts.ToList();

                return View(con);
            }
        }

        [Authorize]
        public ActionResult FriendRequest(int ProfileId)
        {
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                var visitUserProfile = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();

                Profile profile = new Profile();
                profile = visitUserProfile;

                Contact contact = new Contact
                {
                    User = currentUser.Profile,
                    Request = true,
                    Accept = false,
                    Friend = profile
                };

                Contact contact2 = new Contact
                {
                    User = profile,
                    Request = true,
                    Accept = false,
                    Friend = currentUser.Profile
                };

                db.Contacts.Add(contact);
                db.Contacts.Add(contact2);
                db.SaveChanges();

                return RedirectToAction("VisitProfile", "Account", new { profileid = ProfileId });
            }   
        }
    }
}