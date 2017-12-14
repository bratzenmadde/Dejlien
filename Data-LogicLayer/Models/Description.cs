using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Description
    {
        public int DescriptionId { get; set; }
        public string Text { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
