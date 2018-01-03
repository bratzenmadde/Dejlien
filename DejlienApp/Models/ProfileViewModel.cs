using DejlienApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DejlienApp.Models
{
    public class ProfileViewModel
    {
        public Profile Profile { get; set; }
        public PostIndexViewModel  PostIndexViewModel { get; set; }
    }
}