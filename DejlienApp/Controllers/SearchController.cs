using DejlienApp.Models;
using DejlienApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Search(string searchButton, string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return View();
            }
            else
            {
                using (var db = new DataContext())
                {
                    var SearchedProfiles = db.Profiles.Where(n => n.Name.Contains(search) && n.Visible == Visible.Yes);
                    var SearchedP = SearchedProfiles.ToList();

                    if (SearchedP == null)
                    {
                        return View();
                    }

                    else
                    {
                        return View(SearchedP);
                    }
                }
            }
        }
    }
}