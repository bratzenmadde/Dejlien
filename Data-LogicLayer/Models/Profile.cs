using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Profile
    {
        public int ProfilId { get; set; }
        public string Namn { get; set; }
        public int Age { get; set; }
        public string  Location { get; set; }
        public string SearchingFor { get; set; }
        public string Gender { get; set; }
        public string ProfileImage { get; set; }

        public virtual User User { get; set; }
        public virtual Description Description { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Post> Posts { get; set; }
    }
}
