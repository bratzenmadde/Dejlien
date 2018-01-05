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
        public  bool Request { get; set; }
        public bool Accept { get; set; }

        //public virtual Profile User { get; set; }
        public virtual Profile Friend { get; set; }
    }
}
