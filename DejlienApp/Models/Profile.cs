using DejlienApp.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    
    public class Profile : IEntity
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
        
        public string  Location { get; set; }
        
        public string SearchingFor { get; set; }
        
        public string Gender { get; set; }
       
        public byte[] UserPhoto { get; set; }
        
        public string Description { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public ICollection<Interest> Interests { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
