﻿using Data_LogicLayer.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_LogicLayer.Models
{
    public class Post: IEntity
    {
        public int Id { get; set; }
        public int PostAuthor { get; set; }
        public int PostReceiver { get; set; }

        public List<Profile> Profiles { get; set; }
    }
}
