using DejlienApp.Models;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DejlienApp.Controllers
{
    public class PostApiController : ApiController
    {
        [HttpPost]
        public void SavePost(Post post, int receiverId)
        {
            var userId = User.Identity.GetUserId();
            
            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);
                post.Author = currentUser.Profile;
 
                post.Receiver = db.Profiles.Where(p => p.Id == receiverId).SingleOrDefault();

                db.Posts.Add(post);
                db.SaveChanges();
            }
        }
    }
}
