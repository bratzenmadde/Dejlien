using System.Linq;
using System.Web.Mvc;
using DataLogicLayer.Repositories;
using Microsoft.AspNet.Identity;
using DataLogicLayer.Models;
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
                var contacts = db.Contacts.Include(f => f.Friend).Where(c => c.User.Id.ToString() == currentUserId).ToList();

                return View(contacts);
            }
        }

        //Send friendrequest
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
                    Friend = profile,
                    IAskedTheQuestion = true
                };

                Contact contact2 = new Contact
                {
                    User = profile,
                    Request = true,
                    Accept = false,
                    Friend = currentUser.Profile,
                    IAskedTheQuestion = false
                };

                db.Contacts.Add(contact);
                db.Contacts.Add(contact2);
                db.SaveChanges();

                return RedirectToAction("VisitProfile", "Account", new { profileid = ProfileId });
            }   
        }
        
        //Accept friendrequest
        public ActionResult Accept(int ProfileId)
        {
            var userId = User.Identity.GetUserId();
            using (var db = new DataContext())
            {
                var currentUser = db.Users.Include(p => p.Profile).Single(c => c.Id.ToString() == userId);
                var visitUserProfile = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();
  
                var visitCt = db.Contacts.Where(x => x.User.Id == ProfileId).SingleOrDefault(q => q.Friend.Id == currentUser.Id);

                visitCt.Request = false;
                visitCt.Accept = true;
                visitCt.IAskedTheQuestion = false;

                var userCon = db.Contacts.Where(a => a.User.Id == currentUser.Profile.Id).ToList();
                var userCt = userCon.Where(t => t.User.Id == currentUser.Profile.Id).SingleOrDefault(w => w.Friend.Id == ProfileId);

                userCt.Request = false;
                userCt.Accept = true;
                userCt.IAskedTheQuestion = false;

                db.SaveChanges();

                return View("ListOfContacts", userCon);
            }
        }

        //Reject friendrequest
        public ActionResult Reject(int ProfileId)
        {
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                //Tar ut den inloggade användaren
                var currentUser = db.Users.Include(p => p.Profile).Single(c => c.Id.ToString() == userId);
                //Tar ut den användare vi hälsar på
                var visitUserProfile = db.Profiles.Include(e => e.UserAccount).Where(p => p.Id == ProfileId).SingleOrDefault();

                //Tar ut just den som är inloggad och kontakt med den som vi hälsar på
                var visitCt = db.Contacts.Where(x => x.User.Id == ProfileId).SingleOrDefault(q => q.Friend.Id == currentUser.Id);

                visitCt.Request = false;
                visitCt.Accept = false;
                visitCt.IAskedTheQuestion = false;
                
                //Tar ut kontakter för den inloggade
                var userCon = db.Contacts.Where(a => a.User.Id == currentUser.Profile.Id).ToList();
                //Tar ut den som vi hälsar på i den inloggades kontaktlista
                var userCt = userCon.Where(t => t.User.Id == currentUser.Profile.Id).SingleOrDefault(w => w.Friend.Id == ProfileId);

                userCt.Request = false;
                userCt.Accept = false;
                userCt.IAskedTheQuestion = false;

                db.SaveChanges();

                return View("ListOfContacts", userCon);
            }
        }
    }
}