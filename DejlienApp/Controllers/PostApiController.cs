using DejlienApp.Models;
using DejlienApp.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace DejlienApp.Controllers
{
    public class PostApiController : ApiController
    {
        public class PostModel
        {
            public int Reciever { get; set; }
            public string Text { get; set; }
        }

        [HttpPost]
        public void SavePost(PostModel model)
        {
            var userId = User.Identity.GetUserId();
            Post post = new Post();

            using (var db = new DataContext())
            {
                var currentUser = db.Users.Single(c => c.Id.ToString() == userId);

                post.Author = currentUser.Profile;
                post.Receiver = db.Profiles.Where(p => p.Id == model.Reciever).SingleOrDefault();

                post.Text = model.Text;

                db.Posts.Add(post);
                db.SaveChanges();

                return;
            }
        }
    }
}
