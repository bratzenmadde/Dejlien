using DejlienApp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    public class Contact: IEntity
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
