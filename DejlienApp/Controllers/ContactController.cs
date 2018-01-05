using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;

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
                var currentUserProfile = db.Profiles.Single(p => p.UserAccount.ToString() == currentUserId);
                // gets the current users contacts
                var contacts = db.Contacts.Where(c => c.Id == currentUserProfile.Id);
                contacts.ToList();

                return View(contacts);
            }

        }
    }
}