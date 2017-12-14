﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public string InterestName { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
