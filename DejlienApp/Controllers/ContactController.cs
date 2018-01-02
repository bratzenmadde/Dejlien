using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DejlienApp.Repositories;

namespace DejlienApp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult ListOfContacts()
        {
            using (var db = new DataContext())
            {
                var contacts = db.Profiles.ToList();
                return View(contacts);
            }

        }
    }
}