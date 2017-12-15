using DejlienApp.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DejlienApp.Models
{
    public class Interest:IEntity
    {
        public int Id { get; set; }
        public string InterestName { get; set; }

        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
