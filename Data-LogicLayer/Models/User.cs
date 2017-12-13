using Data_LogicLayer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DejlienApp.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}