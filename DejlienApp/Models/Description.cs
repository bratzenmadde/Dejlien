using DejlienApp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    public class Description:IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ProfileId { get; set; }//Bör den finnas på 1:1 samband?
        public virtual Profile Profile { get; set; }
    }
}
