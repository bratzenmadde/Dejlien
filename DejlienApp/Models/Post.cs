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
        public string Text { get; set; }
        public virtual Profile Author { get; set; }
        public virtual Profile Receiver { get; set; }

    }
}
