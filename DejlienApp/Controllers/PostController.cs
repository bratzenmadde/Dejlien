using DejlienApp.Models;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DejlienApp.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(int id)
        {
            using (var db = new DataContext())
            {
                var posts = db.Posts.Where(x => x.Receiver.Id == id).ToList();
                return View(new Profile { Id = id, Posts = posts });
            }
        }
    }

    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Post Post { get; set; } = new Post();
    }
}