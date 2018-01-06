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

    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Post Post { get; set; } = new Post();
    }

    public class ContactViewModel
    {
        public int Id { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public Contact Contact { get; set; } = new Contact();
    }
}