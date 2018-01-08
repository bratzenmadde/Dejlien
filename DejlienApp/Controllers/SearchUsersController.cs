using DataLogicLayer.Models;
using DataLogicLayer.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class SearchUsersController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new DataContext())
            {
                var users = db.Profiles.ToList();
                return View("SearchUsers", users);
            }
        }

        [Authorize]
        public ActionResult SearchUsers(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return View();
            }
            else
            {
                using (var db = new DataContext())
                {
                    var SearchedProfiles = db.Profiles.Where(n => n.Name.Contains(search) && n.Visible == Visible.Yes).ToList();

                    if (SearchedProfiles == null)
                    {
                        return View();
                    }

                    else
                    {
                        return View(SearchedProfiles);
                    }
                }
            }
        }
    }
}