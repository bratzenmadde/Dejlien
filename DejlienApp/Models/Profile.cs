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
    public class Profile
    {
        [Key]
        [ForeignKey("UserAccount")]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string  Location { get; set; }
        [Required]
        public string SearchingFor { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string ProfileImage { get; set; }
        [Required]
        public string Description { get; set; }

       //[ForeignKey("UserAccount")]
        //public int UserId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        public List<Interest> Interests { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Post> Posts { get; set; }
    }
}
