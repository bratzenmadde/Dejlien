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
        
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }
        
        [Required]
        public int Age { get; set; }

        [Required]
        public string  Location { get; set; }
        
        [Required]
        public string SearchingFor { get; set; }
        
        public string Gender { get; set; }
       
        public byte[] UserPhoto { get; set; }
        
        [Required]
        public string Description { get; set; }

        public Visible Visible { get; set; }

        public virtual UserAccount UserAccount { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    public enum Visible { Yes, No }
}
