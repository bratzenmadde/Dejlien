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
                return View(new PostIndexViewModel { Id = id, Posts = posts });
            }
        }


        public ActionResult Create(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post, int id, HttpPostedFileBase upload)
        {
            var userId = User.Identity.GetUserId();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(u => u.Id.ToString() == userId);
                var userProfile = currentUser.Profile;


                post.Author = userProfile;

                var toUser = db.Profiles.Single(x => x.Id == id);
                post.Receiver = toUser;

                db.Posts.Add(post);

                if (upload != null && upload.ContentLength > 0)
                {
                    post.Filename = upload.FileName;
                    post.ContentType = upload.ContentType;

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        post.File = reader.ReadBytes(upload.ContentLength);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
        }

        public ActionResult Image(int id)
        {
            using (var db = new DataContext())
            {
                var post = db.Posts.Single(x => x.Id == id);

                return File(post.File, post.ContentType);
            }
        }
    }

    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}