using DejlienApp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    public class Post: IEntity
    {
        public int Id { get; set; }
        public int PostAuthor { get; set; }
        public int PostReceiver { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}
