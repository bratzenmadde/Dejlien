using Data_LogicLayer.Framework;
using Data_LogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Data_LogicLayer.Models
{
    public class User:IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual Profile Profile { get; set; }
    }

    
}