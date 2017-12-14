using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string Category { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
