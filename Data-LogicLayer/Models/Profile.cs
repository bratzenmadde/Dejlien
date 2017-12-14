using Data_LogicLayer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Profile: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string  Location { get; set; }
        public string SearchingFor { get; set; }
        public string Gender { get; set; }
        public string ProfileImage { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual Description Description { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Post> Posts { get; set; }
    }
}
