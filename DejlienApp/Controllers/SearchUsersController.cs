using DejlienApp.Models;
using DejlienApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class SearchUsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
                    //var SearchedP = SearchedProfiles.ToList();

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